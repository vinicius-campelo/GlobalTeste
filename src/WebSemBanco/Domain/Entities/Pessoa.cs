using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebSemBanco.Domain.Entities
{
    public class Pessoa
    {
        [Key]
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Data")]
        public string DtNascimento { get; set; }

        public Pessoa(int codigo, string nome, string cpf, string uf, string dtNascimento)
        {
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
            Uf = uf.ToUpper();
            DtNascimento = Convert.ToDateTime(dtNascimento).ToString("dd-MM-yyyy");
        }

        public Pessoa()
        {
        }
    }
}
