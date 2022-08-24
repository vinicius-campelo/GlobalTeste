using System.ComponentModel.DataAnnotations;

namespace WebSemBanco.Domain.Entities
{
    public partial class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
    }


    public class TokenModel
    {
        [Required(ErrorMessage = "UserName é obrigatório!")]
        [StringLength(150, ErrorMessage = "UserName deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório!")]
        [StringLength(50, ErrorMessage = "Password deve ter no maximo {1} caracteres.", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
