using HFT.Repository.Interfaces;
using HFT.Models;

namespace HFT.Repository.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(CarDbContext ctx) : base(ctx)
        {
        }
    }
}
