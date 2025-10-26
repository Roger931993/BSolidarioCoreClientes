#region References
using AutoMapper;
using Core.Clientes.Application.DTOs;
using Core.Clientes.Domain.Entities;
#endregion


namespace Core.Clientes.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<cliente, clienteDto>();
            CreateMap<contacto, contactoDto>();
                        
            
        }
    }
}
