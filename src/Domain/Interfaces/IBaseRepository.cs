using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Post(T item);
        void Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<object> GetById(T item);
        Task<T> Put(T item);
    }
}
