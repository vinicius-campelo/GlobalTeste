using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebSemBanco.Data.Utils;
using WebSemBanco.Domain.Entities;
using WebSemBanco.Domain.Interfaces;
using static WebSemBanco.Data.Utils.SigningConfigurations;

namespace WebSemBanco.Data.Repositories
{
    public class AutenticacaoRepository : IAutenticacao
    {
        private SigningHSConfigurations _signingConfiguration;
        private TokenConfigurations _tokenConfigurations;
        private IUsuario _interfaceUsuario;

        public AutenticacaoRepository(
            SigningHSConfigurations signingConfiguration,
            TokenConfigurations tokenConfigurations, IUsuario interfaceUsuario)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfigurations = tokenConfigurations;
            _interfaceUsuario = interfaceUsuario;
        }

        public async Task<object> Autenticacao(Usuario usuario)
        {
            var _usuario = new Usuario();
         
            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.UserName)
                && !string.IsNullOrWhiteSpace(usuario.Password))
            {
                _usuario = _interfaceUsuario.GetAll().
                    FirstOrDefault(p => p.UserName == usuario.UserName && p.Password == usuario.Password);


                if (_usuario == null)
                {
                    return false;
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(_usuario.UserName),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, _usuario.Email),
                            new Claim(JwtRegisteredClaimNames.FamilyName, _usuario.Nome),
                            new Claim(JwtRegisteredClaimNames.NameId, _usuario.Id.ToString()),
                        }
                    );

                    // tempo de duração do token
                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromMinutes(_tokenConfigurations.Minutes);

                    var handler = new JwtSecurityTokenHandler();
                    string token = await CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, _usuario);

                }

            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Cria o token de usuario
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="createDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        private Task<string> CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            return Task.FromResult(handler.WriteToken(securityToken));

        }


        /// <summary>
        /// Retorna as informações de Header do usuario logado.
        /// </summary>
        /// <param name="createDate"></param>
        /// <param name="expirationDate"></param>
        /// <param name="token"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, Usuario user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                Name = user.Nome,
                message = "Usuario logado com sucesso!"
            };
        }
    }
}
