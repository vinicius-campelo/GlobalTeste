using System.Collections.Generic;
using System.Threading.Tasks;
using WebSemBanco.Domain.Entities;

namespace WebSemBanco.Domain.Interfaces
{
    public interface IPessoa
    {
        Task<Pessoa> PostCode(Pessoa item);
        Task<bool> DeleteCode(int codigo);
        Task<IEnumerable<Pessoa>> GetAllCode();
        Task<object> GetIdCode(int codigo);
        Task<object> GetUfCode(string uf);
        Task<bool> PutCode(Pessoa item);
       
    }
}
