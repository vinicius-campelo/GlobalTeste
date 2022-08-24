using Aplication.ViewModels;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IAutenticacaoService
    {
        // ESPECIFICOS AQUI!
        Task<object> Autenticacao(UsuarioViewModel usuario);
    }
}
