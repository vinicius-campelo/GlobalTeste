using Microsoft.EntityFrameworkCore;
using WebSemBanco.Domain.Entities;

namespace WebSemBanco.Data
{
    public class DataBaseConfig : DbContext
    {

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public DataBaseConfig(DbContextOptions<DataBaseConfig> options) : base(options)
        {
        }
    }
}
