using HFT.Repository.Interfaces;
using HFT.Logic.Interfaces;
using HFT.Models;
using System;
using System.Collections.Generic;

namespace HFT.Logic.Services
{
    public class BrandLogic : IBrandLogic
    {
        IBrandRepository brandRepo;
        public BrandLogic(IBrandRepository brandRepo)
        {
            this.brandRepo = brandRepo;
        }

        public void Create(Brand brand)
        {
            brandRepo.Create(brand);
        }

        public void Delete(int id)
        {
            brandRepo.Delete(id);
        }

        public Brand Read(int id)
        {
            Brand brand = brandRepo.Read(id);
            if (brand == null)
                throw new ArgumentException("Brand with the specified id does not exists.");
            return brand;

            //return brandRepo.Read(id) ?? throw new ArgumentException("Brand with the specified id does not exists.");
        }

        public IEnumerable<Brand> ReadAll()
        {
            return brandRepo.ReadAll();
        }

        public void Update(Brand brand)
        {
            brandRepo.Update(brand);
        }
    }
}
