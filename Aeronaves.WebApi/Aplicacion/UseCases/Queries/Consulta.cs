using Aeronaves.WebApi.Aplicacion.Dto;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Aeronaves.WebApi.Persistencia;
using Aeronaves.WebApi.Modelo;
using Microsoft.EntityFrameworkCore;

namespace Aeronaves.WebApi.Aplicacion.UseCases.Queries
{
    public class Consulta
    {
        public class ListaAeronave : IRequest<List<AeronaveDto>>
        {
        }

        public class Manejador : IRequestHandler<ListaAeronave, List<AeronaveDto>>
        {
            private readonly ContextoAeronave _contexto;
            private readonly IMapper _mapper;

            public Manejador(ContextoAeronave contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
            public async Task<List<AeronaveDto>> Handle(ListaAeronave request, CancellationToken cancellationToken)
            {

                var aeronave = await _contexto.Aeronave.ToListAsync();
                var aeronaveDto = _mapper.Map<List<Aeronave>, List<AeronaveDto>>(aeronave);
                return aeronaveDto;
            }
        }



    }
}
