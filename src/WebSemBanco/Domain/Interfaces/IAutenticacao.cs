using System.Threading.Tasks;
using WebSemBanco.Domain.Entities;

namespace WebSemBanco.Domain.Interfaces
{
    public interface IAutenticacao
    {
        Task<object> Autenticacao(Usuario usuario);
    }
}
