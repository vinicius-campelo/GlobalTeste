using Aplication.Interfaces;
using Aplication.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private IAutenticacaoRepository _autenticacaoRepository;
        private IMapper _mapper;

        public AutenticacaoService(IMapper mapper, 
            IAutenticacaoRepository autenticacaoRepository)
        {
            _autenticacaoRepository = autenticacaoRepository;
            _mapper = mapper;
        }
        public async Task<object> Autenticacao(UsuarioViewModel usuario)
        {
            var mapResult = _mapper.Map<Usuario>(usuario);
            return await _autenticacaoRepository.Autenticacao(mapResult);
        }
    }
}
