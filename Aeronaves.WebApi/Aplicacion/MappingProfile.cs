using Aeronaves.WebApi.Aplicacion.Dto;
using Aeronaves.WebApi.Modelo;
using AutoMapper;

namespace Aeronaves.WebApi.Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Aeronave, AeronaveDto>();
            //CreateMap<AeronaveAsientos, AeronaveAsientosDto>();
        }
    }
}
