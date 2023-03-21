using CentralBank.Models;

namespace CentralBank.Data
{
    public class ReferenceIndexRepo : IReferenceIndexRepo
    {
        private readonly AppDbContext _context;

        public ReferenceIndexRepo(AppDbContext context)
        {
            _context = context;
        }

        public bool SaveChanges() => (_context.SaveChanges() >= 0);

        public IEnumerable<ReferenceIndex> GetReferenceIndexes() => _context.Indexes.ToList();

        public ReferenceIndex GetReferenceIndexById(int id)
        {
            return _context.Indexes.FirstOrDefault(e => e.Id == id) ?? throw new InvalidOperationException();
        }

        public void CreateReferenceIndex(ReferenceIndex index)
        {
            if (index == null)
                throw new ArgumentNullException(nameof(index));

            _context.Indexes.Add(index);
        }
    }
}
