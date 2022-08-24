using System.Collections.Generic;
using WebSemBanco.Domain.Entities;

namespace WebSemBanco.Domain.Interfaces
{
    public interface IUsuario
    {
        IEnumerable<Usuario> GetAll();
    }
}
