using Dapper;
using Dapper.Contrib.Extensions;
using Data.Context;
using Domain;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    // crud generico
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DataBaseConfig databaseConfig;
        public PessoaRepository(DataBaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async void Delete(int id)
        {
            using (var connection = new SqliteConnection(databaseConfig.ConnectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = new StringBuilder();
                        sql.AppendLine($@"DELETE FROM Pessoa WHERE Codigo = {id}");
                        await connection.ExecuteAsync(sql.ToString(), transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public async Task<IEnumerable<Pessoa>> GetAll()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Pessoa";
            var result = await connection.QueryAsync<Pessoa>(query);

            var returnList = new List<Pessoa>();

            foreach (var PessoaSalva in result.ToList())
            {
                var pessoa = new Pessoa(

                    PessoaSalva.Codigo,
                    PessoaSalva.Nome,
                    PessoaSalva.Cpf,
                    PessoaSalva.Uf,
                    PessoaSalva.DtNascimento

                );
                returnList.Add(pessoa);
            }

            return returnList.ToList();
        }

        public async Task<object> GetById(Pessoa item)
        {

            var list = await GetAll();

            if (item.Codigo > 0)
            {
                 return list.Where(obj => obj.Codigo.Equals(item.Codigo)).FirstOrDefault();
            }
            else
            {
                return list.Where(obj => obj.Uf.Equals(item.Uf)).ToList();
            }

        }

        public async Task<Pessoa> Post(Pessoa item)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            connection.Open();

            var list = await GetAll();

            var result = GetAll().Result.ToList()
              .Where(p => p.Cpf.Equals(item.Cpf)).FirstOrDefault();

            if(result == null)

                item.Codigo = ((list.Max(i => (int?)i.Codigo) ?? 0) + 1);

                item.Codigo = await connection.InsertAsync(item);

            return item.Export();
        }


        public async Task<Pessoa> Put(Pessoa item)
        {
            var result = GetAll().Result.Where(p => p.Codigo == item.Codigo).FirstOrDefault();

            var _updatePessoa = new Pessoa
            {
                Codigo = result.Codigo,
                Cpf = result.Cpf.Equals(item.Cpf) ? result.Cpf : item.Cpf,
                Uf = result.Uf.Equals(item.Uf) ? result.Uf : item.Uf,
                Nome = result.Nome.Equals(item.Nome) ? result.Nome : item.Nome,
                DtNascimento = result.DtNascimento.Equals(item.DtNascimento)
                ? result.DtNascimento : item.DtNascimento,
            };


            using (var connection = new SqliteConnection(databaseConfig.ConnectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sql = new StringBuilder();
                        sql.AppendLine($@"UPDATE Pessoa 
                        SET Nome = '{ _updatePessoa.Nome }',
                        Cpf = '{ _updatePessoa.Cpf }', 
                        Uf = '{ _updatePessoa.Uf }', 
                        DtNascimento = '{  _updatePessoa.DtNascimento }' 
                        WHERE Codigo = { _updatePessoa.Codigo };");

                        await connection.ExecuteAsync(sql.ToString(), transaction);

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }

            return item.Export();
        }
    }
}
