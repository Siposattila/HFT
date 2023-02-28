using HFT.Repository.Interfaces;
using HFT.Logic.Interfaces;
using HFT.Models;
using System;
using System.Collections.Generic;

namespace HFT.Logic.Services
{
    public class OwnerLogic : IOwnerLogic
    {
        IOwnerRepository ownerRepo;
        public OwnerLogic(IOwnerRepository ownerRepo)
        {
            this.ownerRepo = ownerRepo;
        }

        public void Create(Owner owner)
        {
            ownerRepo.Create(owner);
        }

        public void Delete(int id)
        {
            ownerRepo.Delete(id);
        }

        public Owner Read(int id)
        {
            Owner brand = ownerRepo.Read(id);
            if (brand == null)
                throw new ArgumentException("Brand with the specified id does not exists.");
            return brand;

            //return brandRepo.Read(id) ?? throw new ArgumentException("Brand with the specified id does not exists.");
        }

        public IEnumerable<Owner> ReadAll()
        {
            return ownerRepo.ReadAll();
        }

        public void Update(Owner owner)
        {
            ownerRepo.Update(owner);
        }
    }
}
