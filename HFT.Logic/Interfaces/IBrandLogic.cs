using HFT.Models;
using System.Collections.Generic;

namespace HFT.Logic.Interfaces
{
    public interface IBrandLogic
    {
        void Create(Brand brand);
        void Delete(int id);
        IEnumerable<Brand> ReadAll();
        Brand Read(int id);
        void Update(Brand brand);
    }
}
