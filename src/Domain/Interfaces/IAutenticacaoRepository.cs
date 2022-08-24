using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAutenticacaoRepository
    {
        Task<object> Autenticacao(Usuario usuario);
    }
}
