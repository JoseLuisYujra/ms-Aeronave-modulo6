using AutoMapper;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System;
using Aeronaves.WebApi.Aplicacion.Dto;
using Aeronaves.WebApi.Modelo;
using Aeronaves.WebApi.Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Aeronaves.WebApi.Aplicacion.UseCases.Queries
{
    public class ConsultaFiltro
    {
        public class AeronveUnico : IRequest<AeronaveDto>
        {
            public Guid AeronaveGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AeronveUnico, AeronaveDto>
        {
            private readonly ContextoAeronave _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAeronave contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AeronaveDto> Handle(AeronveUnico request, CancellationToken cancellationToken)
            {
                var aeronave = await _contexto.Aeronave.Where(x => x.AeronaveId == request.AeronaveGuid).FirstOrDefaultAsync();
                if (aeronave == null)
                {
                    throw new Exception("No se encontro la Aeronave");
                }

                var aeronaveDto = _mapper.Map<Aeronave, AeronaveDto>(aeronave);

                return aeronaveDto;
            }
        }

    }
}
