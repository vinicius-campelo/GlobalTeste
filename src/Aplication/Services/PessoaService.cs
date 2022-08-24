using Aplication.Interfaces;
using Aplication.ViewModels;
using AutoMapper;
using Domain;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.Services
{
   
    public class PessoaService : IPessoaService
    {
        private IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;
        

        public PessoaService(IMapper mapper, IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            _pessoaRepository.Delete(id);
        }

        public async Task<object> GetAll()
        {
            var result = await _pessoaRepository.GetAll();
            return _mapper.Map<IEnumerable<object>>(result);

        }

        public async Task<object> GetByCodigo(PessoaViewModel item)
        {
            var _item = new Pessoa { Codigo = item.Codigo, Uf = item.Uf };

            var result = await _pessoaRepository.GetById(_item);
            return _mapper.Map<object>(result);
        }


        public object Update(PessoaViewModel item)
        {
          var mapResult = _mapper.Map<Pessoa>(item);
          return _pessoaRepository.Put(mapResult).Result;
        }

        public object Post(PessoaViewModel item)
        {
            var mapResult = _mapper.Map<Pessoa>(item);
            return _pessoaRepository.Post(mapResult).Result;
            
        }
    }
}
