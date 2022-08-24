using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }

        private string _role;

        /* Administrador, Usuário Comum */
        public string Role
        {
            get { return _role == "1" ? "Administrador" : "Usuário Comum"; }
            set { _role = value; }
        }
    }
}
