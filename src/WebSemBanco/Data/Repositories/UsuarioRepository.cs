using System.Collections.Generic;
using WebSemBanco.Domain.Entities;
using WebSemBanco.Domain.Interfaces;

namespace WebSemBanco.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuario
    {
        public UsuarioRepository(DataBaseConfig context) : base(context)
        {
        }

        public  IEnumerable<Usuario> GetAll()
        {

            var listaUsuarios =  new List<Usuario>();
                listaUsuarios.AddRange(new[] {
                new Usuario{ Id = 1, UserName = "VXPTO", Password = "123456", Nome = "Vinicius Campelo", Email = "autanbr@gmail.com" },
                new Usuario{ Id = 1, UserName = "ADM", Password = "a123", Nome = "Administrador", Email = "adm@email.com" },
            });
            return listaUsuarios;
        }
    }
}
