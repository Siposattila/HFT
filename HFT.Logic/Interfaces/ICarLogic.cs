using HFT.Models;
using System.Collections.Generic;
using System.Linq;

namespace HFT.Logic.Interfaces
{
    public interface ICarLogic
    {
        void Create(Car car);
        void Delete(int id);
        IEnumerable<Car> ReadAll();
        Car Read(int id);
        void Update(Car car);
        double AVGPrice();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByBrands();
        IEnumerable<KeyValuePair<string, double>> AVGPriceByOwners();
        IEnumerable<string> OwnersByBrandName(string name);
        IEnumerable<KeyValuePair<string, string>> GetOwnersWithCars();
        IEnumerable<KeyValuePair<string, string>> GetCarsByBrands();
    }
}
