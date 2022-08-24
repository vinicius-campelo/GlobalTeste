using Aplication.Interfaces;
using Aplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Web.Controller
{

    [Route("[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private IAutenticacaoService _autenticacaoService;

        public AutenticacaoController(IAutenticacaoService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody]AutenticacaoViewModel autenticacaoViewModel)
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

                var usuario = new UsuarioViewModel()
                {
                    UserName = autenticacaoViewModel.UserName,
                    Password = autenticacaoViewModel.Password
                };

                var result = _autenticacaoService.Autenticacao(usuario).Result;

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
