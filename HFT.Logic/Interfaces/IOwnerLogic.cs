using HFT.Models;
using System.Collections.Generic;

namespace HFT.Logic.Interfaces
{
    public interface IOwnerLogic
    {
        void Create(Owner owner);
        void Delete(int id);
        IEnumerable<Owner> ReadAll();
        Owner Read(int id);
        void Update(Owner owner);
    }
}
