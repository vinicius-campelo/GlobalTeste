using System.Threading.Tasks;

namespace Aplication.Interfaces
{

    // BASE (generico)
    public interface IBaseService<T> where T : class
    {
        object Post(T item);
        void Delete(int id);
        Task<object> GetAll();
        Task<object> GetByCodigo(T item);
        object Update(T item);
    }
}
