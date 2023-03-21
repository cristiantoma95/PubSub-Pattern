using CentralBank.Dtos;

namespace CentralBank.DataSynchronizer
{
    public interface IPublisher
    {
        Task PublishData(ReferenceIndexCreateDto referenceIndexCreate);
    }
}
