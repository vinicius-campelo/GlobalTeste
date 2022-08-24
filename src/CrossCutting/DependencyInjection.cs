
using Aplication.Interfaces;
using Aplication.Services;
using Data.Context;
using Data.Repositories;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static Data.Utils.SigningConfigurations;

namespace CrossCutting
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
          IConfiguration configuration)
        {
            services.AddSingleton(new DataBaseConfig { ConnectionString = configuration.GetConnectionString("DefaultConnection") });
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IPessoaService, PessoaService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddScoped<IAutenticacaoRepository, AutenticacaoRepository>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            var signingConfigurations = new SigningHSConfigurations(configuration);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
               };
           });

            return services;
        }
    }
}
