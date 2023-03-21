using AutoMapper;
using CentralBank.Data;
using CentralBank.DataSynchronizer;
using CentralBank.Dtos;
using CentralBank.Models;
using Microsoft.AspNetCore.Mvc;

namespace CentralBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentralBankController : ControllerBase
    {
        private readonly IReferenceIndexRepo _repository;
        private readonly IMapper _mapper;
        private readonly IPublisher _publisher;

        public CentralBankController(IReferenceIndexRepo repository, IMapper mapper, IPublisher publisher)
        {
            _repository = repository;
            _mapper = mapper;
            _publisher = publisher;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReferenceIndexReadDto>> GetReferenceIndexes()
        {
            var referenceIndexes = _repository.GetReferenceIndexes();

            return Ok(_mapper.Map<IEnumerable<ReferenceIndexReadDto>>(referenceIndexes));
        }

        [HttpGet("{id}", Name = "GetReferenceIndexById")]
        public ActionResult<ReferenceIndexReadDto> GetReferenceIndexById(int id)
        {
            try
            {
                var referenceIndex = _repository.GetReferenceIndexById(id);

                if (referenceIndex != null)
                    return Ok(_mapper.Map<ReferenceIndexReadDto>(referenceIndex));

                return NotFound();
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e);
            }
        }

        [HttpPost("index")]
        public async Task<ActionResult> CreateReferenceIndex([FromBody] ReferenceIndexCreateDto referenceIndexCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var referenceIndex = _mapper.Map<ReferenceIndex>(referenceIndexCreate);
                _repository.CreateReferenceIndex(referenceIndex);
                var isSuccess = _repository.SaveChanges();

                if (!isSuccess)
                    return BadRequest("Could not create ReferenceIndex");

                await _publisher.PublishData(referenceIndexCreate);
                return Created("", referenceIndexCreate);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
