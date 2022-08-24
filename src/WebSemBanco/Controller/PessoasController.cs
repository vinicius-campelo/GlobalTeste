using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebSemBanco.Domain.Entities;
using WebSemBanco.Domain.Interfaces;

namespace WebSemBanco.Controller
{

    [Route("[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoa _pessoaRepository;

        public PessoasController(IPessoa pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pessoa = await _pessoaRepository.GetAllCode();

            if (pessoa == null)
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
                Message = pessoa
            });
        }


        [Authorize("Bearer")]
        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> GetById(int codigo)
        {

            if (codigo == 0)
            {
                return BadRequest(new
                {
                    StatusCodeResult = StatusCode(400),
                    Message = "Erro do cliente",
                    Erros = ModelState.Values.SelectMany(p => p.Errors)
                });
            }


            var result = await _pessoaRepository.GetIdCode(codigo);

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
        public async Task<IActionResult> GetByUF(string uf)
        {

            if (string.IsNullOrEmpty(uf))
            {
                return BadRequest(new
                {
                    StatusCodeResult = StatusCode(400),
                    Message = "Erro do cliente",
                    Erros = ModelState.Values.SelectMany(p => p.Errors)
                });
            }


            var result = await _pessoaRepository.GetUfCode(uf);

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
        public IActionResult Post([FromBody]Pessoa  pessoa)
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
                var result = _pessoaRepository.PostCode(pessoa);

                return Ok(new
                {
                    StatusCodeResult = StatusCode(201),
                    Message = result
                });
            }
        }


        [Authorize("Bearer")]
        [HttpPut]
        public IActionResult Put([FromBody]Pessoa pessoa)
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

            var result = _pessoaRepository.PutCode(pessoa).Result;

            if (result == false)
            
                return NotFound(new
                {
                    StatusCodeResult = StatusCode(404),
                    Message = "O servidor não pode encontrar o recurso solicitado!"
                });
            
          
            return Ok(new
            {
                StatusCodeResult = StatusCode(201),
                Message = pessoa
            });
        }
    }
}
