using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{

    [Table("Pessoa")]
    public class Pessoa
    {
        [Key]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Uf { get; set; }
        public string DtNascimento { get; set; }

        public Pessoa(int codigo, string nome, string cpf, string uf, string dtNascimento)
        {
            Codigo = codigo;
            Nome = nome;
            Cpf = cpf;
            Uf = uf.ToUpper();
            DtNascimento = Convert.ToDateTime(dtNascimento).ToString("yyyy-MM-dd");
        }

        public Pessoa()
        {

        }

        public Pessoa Export() => new Pessoa(Codigo, Nome, Cpf, Uf, DtNascimento);

    }

}
