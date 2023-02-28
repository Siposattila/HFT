using HFT.Repository.Interfaces;
using HFT.Models;

namespace HFT.Repository.Repositories
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(CarDbContext ctx) : base(ctx)
        {
        }
    }
}
