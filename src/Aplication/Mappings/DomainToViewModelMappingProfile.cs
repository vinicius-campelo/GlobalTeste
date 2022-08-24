using Aplication.ViewModels;
using AutoMapper;
using Domain.Entities;


namespace Aplication.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        // Separação das responsabilidades usando AutoMapper
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Pessoa, PessoaViewModel>();
            CreateMap<Usuario, UsuarioViewModel>();
           
        }
    }
}
