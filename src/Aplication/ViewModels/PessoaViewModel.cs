using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Aplication.ViewModels
{
    public class PessoaViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O NOME é requerido!")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido!")]
        [MaxLength(14)]
        [DisplayName("CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "A UF e requerido!")]
        [MinLength(2)]
        [MaxLength(2)]
        [DisplayName("UF")]
        public string Uf { get; set; }

        [Required(ErrorMessage = "A DATA NASCIMENTO é requerido!")]
        [DataType(DataType.Date, ErrorMessage = "Data esta em um formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data")]
        public DateTime DtNascimento { get; set; }
    }
}
