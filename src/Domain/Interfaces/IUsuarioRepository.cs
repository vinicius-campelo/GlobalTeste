using Domain.Entities;
using System.Collections.Generic;

namespace Domain
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> GetAll();
    }
}
