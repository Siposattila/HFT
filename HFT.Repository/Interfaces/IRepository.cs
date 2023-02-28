using HFT.Models;
using System.Linq;

namespace HFT.Repository.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IQueryable<T> ReadAll();
        T Read(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
