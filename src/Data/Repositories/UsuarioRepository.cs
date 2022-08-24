using Domain;
using Domain.Entities;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
  
        public IEnumerable<Usuario> GetAll()
        {
            var listaUsuarios = new List<Usuario>();
            listaUsuarios.AddRange(new[] {
                new Usuario{ Id = 1, UserName = "VXPTO", Password = "123456", Nome = "Vinicius Campelo", Email = "autanbr@gmail.com", Role = "2"},
                new Usuario{ Id = 1, UserName = "ADM", Password = "a123", Nome = "Administrador", Email = "adm@email.com",  Role = "1"},
            });
            return listaUsuarios;
        }
    }
}
