using Aplication.ViewModels;
using AutoMapper;
using Domain.Entities;


namespace Aplication.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        // Separação das responsabilidades usando AutoMapper
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<PessoaViewModel, Pessoa>();
            CreateMap<UsuarioViewModel, Usuario>();
        }
    }
}
