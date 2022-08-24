using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSemBanco.Data;
using WebSemBanco.Data.Repositories;
using WebSemBanco.Domain.Interfaces;

namespace WebSemBanco.CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataBaseConfig>(p => p.UseInMemoryDatabase("Database"));
            services.AddScoped<IPessoa, PessoaRepository>();
            services.AddScoped<IUsuario, UsuarioRepository>();
            services.AddScoped<IAutenticacao, AutenticacaoRepository>();

            return services;
        }
    }
}
