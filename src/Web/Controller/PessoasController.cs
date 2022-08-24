using Aplication.Interfaces;
using Aplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;


namespace Web.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }


        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _pessoaService.GetAll();

            if (result == null)
            {
                return NotFound(new
                {
                    StatusCodeResult = StatusCode(404),
                    Message = "O servidor não pode encontrar o recurso solicitado!"
                });
            }

            return Ok(new
            {
                StatusCodeResult = StatusCode(200),
                Message = result
            });
        }


        [Authorize("Bearer")]
        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetId(int codigo)
        {
            var item = new PessoaViewModel { Codigo = codigo };
            var result = await _pessoaService.GetByCodigo(item);

            if (result == null)
            {
                return NotFound(new
                {
                    StatusCodeResult = StatusCode(404),
                    Message = "O servidor não pode encontrar o recurso solicitado!"
                });
            }

            return Ok(new
            {
                StatusCodeResult = StatusCode(200),
                Message = result
            });
        }


        [Authorize("Bearer")]
        [HttpGet("uf/{uf}")]
        public async Task<IActionResult> GetUf(string uf)
        {
            var item = new PessoaViewModel { Uf = uf.ToUpper() };
            var result = await _pessoaService.GetByCodigo(item);

            if (result == null)
            {
                return NotFound(new
                {
                    StatusCodeResult = StatusCode(404),
                    Message = "O servidor não pode encontrar o recurso solicitado!"
                });
            }

            return Ok(new
            {
                StatusCodeResult = StatusCode(200),
                Message = result
            });
        }


        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody] PessoaViewModel pessoaViewModel)
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
                var result = _pessoaService.Post(pessoaViewModel);

                return Ok(new
                {
                    StatusCodeResult = StatusCode(201),
                    Message = result
                });
            }
        }


        [Authorize("Bearer")]
        [HttpPut("update")]
        public IActionResult Put([FromBody]PessoaViewModel pessoaViewModel)
        {
            
            var item = new PessoaViewModel { Codigo = pessoaViewModel.Codigo };
            var retorno = _pessoaService.GetByCodigo(item);

   
            if (pessoaViewModel.Codigo == 0)
                return BadRequest(new
                {
                    StatusCodeResult = StatusCode(400),
                    Message = "Erro do cliente - código é requerido!"
                });

            object result;
            if (retorno.Result == null)
            {
                return NotFound(new
                {
                    StatusCodeResult = StatusCode(404),
                    Message = "O servidor não pode encontrar o recurso solicitado!"
                });
            }
            else
            {
                result = _pessoaService.Update(pessoaViewModel);
            }

            return Ok(new
            {
                StatusCodeResult = StatusCode(201),
                Message = result
            });
        }


        [Authorize("Bearer")]
        [HttpDelete("delete/{codigo}")]
        public IActionResult Delete(int codigo)
        {
            var item = new PessoaViewModel { Codigo = codigo };
            var result = _pessoaService.GetByCodigo(item).Result;

            if (codigo == 0)
                return BadRequest(new { StatusCodeResult = StatusCode(400), 
                    Message = "Erro do cliente - código é requerido!" });

            if (result == null)
            {
                return NotFound(new { StatusCodeResult = StatusCode(404), 
                    Message = "O servidor não pode encontrar o recurso solicitado!" });
            }
            else
            {
                _pessoaService.Delete(codigo);
            }

            return Ok(new { StatusCodeResult = StatusCode(200), 
                Message = "Deletado com Sucesso!" });
        }
    }
}
