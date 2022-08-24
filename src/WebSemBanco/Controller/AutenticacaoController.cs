using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebSemBanco.Domain.Entities;
using WebSemBanco.Domain.Interfaces;

namespace WebSemBanco.Controller
{

    [Route("[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IAutenticacao _autenticacao;

        public AutenticacaoController(IAutenticacao autenticacao)
        {
            _autenticacao = autenticacao;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] TokenModel tokenModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCodeResult = StatusCode(400),
                    Message = "Erro do cliente",
                    Erros = ModelState.Values.SelectMany(p => p.Errors)
                });

            }
            else
            {

                var usuario = new Usuario()
                {
                    UserName = tokenModel.UserName,
                    Password = tokenModel.Password
                };

                var result = _autenticacao.Autenticacao(usuario).Result;

                if (!result.Equals(false))
                {
                    return Ok(new
                    {
                        StatusCodeResult = StatusCode(201),
                        Message = result
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        StatusCodeResult = StatusCode(404),
                        Message = "Usuário ou senha inválidos!"
                    });

                }
            }
        }
    }
}
