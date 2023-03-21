using AutoMapper;
using CentralBank.Controllers;
using CentralBank.Data;
using CentralBank.DataSynchronizer;
using CentralBank.Dtos;
using CentralBank.Models;
using CentralBank.Profiles;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace PublishSubscribeTests
{
    [TestClass]
    public class CentralBankControllerTests
    {
        private readonly CentralBankController _controller;
        private readonly Mock<IReferenceIndexRepo> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IPublisher> _mockPublish;

        public CentralBankControllerTests()
        {
            var mapProfile = new ReferenceIndexProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapProfile));
            _mapper = new Mapper(configuration);

            _mockRepo = new Mock<IReferenceIndexRepo>();
            _mockPublish = new Mock<IPublisher>();
            _controller = new CentralBankController(_mockRepo.Object, _mapper, _mockPublish.Object);
        }

        [Fact]
        public void GetReferenceIndexes_ReturnsOkResult()
        {
            var timeStamp = DateTime.UtcNow;
            var referenceIndexes = new List<ReferenceIndex>()
            {
                new() { Id = 1, Index = 2, TimeStamp = timeStamp },
                new() { Id = 2, Index = 3, TimeStamp = timeStamp }
            };

            _mockRepo.Setup(repo => repo.GetReferenceIndexes()).Returns(referenceIndexes);

            var referenceIndexReadDtos = _mapper.Map<IEnumerable<ReferenceIndexReadDto>>(referenceIndexes);

            var actionResult = _controller.GetReferenceIndexes();

            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equivalent(referenceIndexReadDtos, okResult.Value);
        }

        [Fact]
        public void GetReferenceIndexById_WithValidId_ReturnsOkResult()
        {
            var timeStamp = DateTime.UtcNow;
            var id = 1;
            var referenceIndex = new ReferenceIndex() {Id = id, Index = 2, TimeStamp = timeStamp};

            _mockRepo.Setup(repo => repo.GetReferenceIndexById(id)).Returns(referenceIndex);

            var referenceIndexReadDto = _mapper.Map<ReferenceIndexReadDto>(referenceIndex);

            var actionResult = _controller.GetReferenceIndexById(id);

            var result = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Equivalent(referenceIndexReadDto, result.Value);
        }

        [Fact]
        public void GetReferenceIndexById_WithInvalidId_ReturnsNotFoundResult()
        {
            const int invalidId = 9999;
            _mockRepo.Setup(repo => repo.GetReferenceIndexById(invalidId)).Returns(null as ReferenceIndex);

            var actionResult = _controller.GetReferenceIndexById(invalidId);

            var result = actionResult.Result as NotFoundResult;
            Assert.Equal(result.StatusCode, 404);
        }

        [Fact]
        public async Task CreateReferenceIndex_ReturnsBadRequestResult_CouldNotSaveInDb()
        {
            var mockIndex = new ReferenceIndexCreateDto
            {
                Index = 2,
                TimeStamp = DateTime.Now
            };

            var referenceIndex = _mapper.Map<ReferenceIndex>(mockIndex);

            _mockRepo.Setup(repo => repo.CreateReferenceIndex(referenceIndex));
            _mockRepo.Setup(repo => repo.SaveChanges()).Returns(false);

            var actionResult = await _controller.CreateReferenceIndex(mockIndex);

            var result = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal(result.StatusCode, 400);
        }

        [Fact]
        public async Task CreateReferenceIndex_ReturnsCreatedResult()
        {
            var mockIndex = new ReferenceIndexCreateDto
            {
                Index = 2,
                TimeStamp = DateTime.Now
            };

            var referenceIndex = _mapper.Map<ReferenceIndex>(mockIndex);

            _mockRepo.Setup(repo => repo.CreateReferenceIndex(referenceIndex));
            _mockRepo.Setup(repo => repo.SaveChanges()).Returns(true);
            _mockPublish.Setup(pub => pub.PublishData(It.IsAny<ReferenceIndexCreateDto>()));

            var actionResult = await _controller.CreateReferenceIndex(mockIndex);

            var result = Assert.IsType<CreatedResult>(actionResult);
            Assert.Equal(mockIndex, result.Value);
            _mockPublish.Verify();
        }
    }
}