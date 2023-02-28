using HFT.Repository.Interfaces;
using HFT.Models;

namespace HFT.Repository.Repositories
{
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        public OwnerRepository(CarDbContext ctx) : base(ctx)
        {
        }
    }
}
