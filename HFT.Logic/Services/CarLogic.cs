using HFT.Repository.Interfaces;
using HFT.Logic.Interfaces;
using HFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HFT.Logic.Services
{
    public class CarLogic : ICarLogic
    {
        ICarRepository carRepo;
        IOwnerRepository ownerRepo;
        IBrandRepository brandRepo;
        public CarLogic(ICarRepository carRepo, IOwnerRepository ownerRepo, IBrandRepository brandRepo)
        {
            this.carRepo = carRepo;
            this.ownerRepo = ownerRepo;
            this.brandRepo = brandRepo;
        }

        public void Create(Car car)
        {
            if (car.Model == string.Empty)
            {
                throw new InvalidOperationException("The model of the car cannot be empty!");
            }

            carRepo.Create(car);
        }

        public void Delete(int id)
        {
            carRepo.Delete(id);
        }

        public Car Read(int id)
        {
            return carRepo.Read(id) ?? throw new ArgumentException("Car with the specified id does not exists.");
        }

        public IEnumerable<Car> ReadAll()
        {
            return carRepo.ReadAll();
        }

        public void Update(Car car)
        {
            carRepo.Update(car);
        }

        public double AVGPrice()
        {
            return carRepo.ReadAll()
                .Average(t => t.BasePrice) ?? -1;
        }

        public IEnumerable<KeyValuePair<string, double>> AVGPriceByBrands()
        {
            return from x in carRepo.ReadAll()
                   group x by x.Brand.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.BasePrice) ?? -1);
        }

        public IEnumerable<KeyValuePair<string, double>> AVGPriceByOwners()
        {
            return from x in carRepo.ReadAll()
                   group x by x.Owner.Name into g
                   select new KeyValuePair<string, double>
                   (g.Key, g.Average(t => t.BasePrice) ?? -1);
        }

        public IEnumerable<string> OwnersByBrandName(string name)
        {
            if (name == string.Empty)
            {
                throw new ArgumentException("The given brand name is invalid!");
            }

            return from c in carRepo.ReadAll()
                where c.Brand.Name == name
                select c.Owner.Name;
        }

        public IEnumerable<KeyValuePair<string, string>> GetOwnersWithCars()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            var o = ownerRepo.ReadAll();
            var c = carRepo.ReadAll();
            foreach (var owner in o)
            {
                var a = c.Where(x => x.Owner.Name == owner.Name).Select(x => x.Model).ToList();
                result.Add(new KeyValuePair<string, string>(owner.Name, string.Join(", ", a)));
            }

            return result;
        }

        public IEnumerable<KeyValuePair<string, string>> GetCarsByBrands()
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            var b = brandRepo.ReadAll();
            var c = carRepo.ReadAll();
            foreach (var brand in b)
            {
                var a = c.Where(x => x.Brand.Name == brand.Name).Select(x => x.Model).ToList();
                result.Add(new KeyValuePair<string, string>(brand.Name, string.Join(", ", a)));
            }

            return result;
        }
    }
}
