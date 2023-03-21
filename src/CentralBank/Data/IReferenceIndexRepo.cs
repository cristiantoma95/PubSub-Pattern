using CentralBank.Models;

namespace CentralBank.Data
{
    public interface IReferenceIndexRepo
    {
        bool SaveChanges();

        IEnumerable<ReferenceIndex> GetReferenceIndexes();

        ReferenceIndex GetReferenceIndexById(int id);

        void CreateReferenceIndex(ReferenceIndex index);
    }
}
