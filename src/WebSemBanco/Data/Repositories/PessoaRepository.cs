using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSemBanco.Domain.Entities;
using WebSemBanco.Domain.Interfaces;

namespace WebSemBanco.Data.Repositories
{
    public class PessoaRepository : BaseRepository<Pessoa>, IPessoa
    {

        public PessoaRepository(DataBaseConfig context) : base(context)
        {
           
        }

        public async Task<bool> DeleteCode(int codigo)
        {

            var result = GetAllCode().Result.ToList()
                .Where(p => p.Codigo.Equals(codigo)).FirstOrDefault();

            if (result == null)
            {
                return false;
            }

            _context.Pessoa.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pessoa>> GetAllCode()
        {

            if (_context.Pessoa.ToList().Count == 0)
            {
                 _context.Pessoa.AddRange(new[] {
                    new Pessoa{ Codigo = 1, Cpf = "890.980.071-20", Uf = "PB", DtNascimento = "1978-07-02", Nome = "Elaine Camila Rocha"  },
                    new Pessoa{ Codigo = 2, Cpf = "278.972.978-61", Uf = "SE", DtNascimento = "1954-03-21", Nome = "Eduardo Severino Henrique Freitas"  },
                 });

                 _context.SaveChanges();
            }

            

            return await _context.Pessoa.ToListAsync();
        }

        public async Task<object> GetIdCode(int codigo)
        {
            return await _context.Pessoa
              .Where(p => p.Codigo.Equals(codigo)).FirstOrDefaultAsync();
        }


        public async Task<object> GetUfCode(string uf)
        {
            return await _context.Pessoa
               .Where(p => p.Uf.Equals(uf.ToUpper())).ToListAsync();
        }

        public async Task<Pessoa> PostCode(Pessoa item)
        {
            var result = GetAllCode().Result.ToList()
                .Where(p => p.Cpf.Equals(item.Cpf)).FirstOrDefault();
       
            if (result != null)
            {
                return item;
            }
            else
            {
                var obj = await _context.Pessoa.AddAsync(item);
                await _context.SaveChangesAsync();
                item.Codigo = obj.Entity.Codigo;
            }

            return item;
      
        }

        public async Task<bool> PutCode(Pessoa item)
        {
            var result = GetAllCode().Result.ToList()
            .Where(p => p.Codigo.Equals(item.Codigo)).FirstOrDefault();

            if (result == null)
            {
                return false;
            }

            _context.Entry(result).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
