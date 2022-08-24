using System.ComponentModel.DataAnnotations;

namespace Aplication.ViewModels
{
    public class AutenticacaoViewModel
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
