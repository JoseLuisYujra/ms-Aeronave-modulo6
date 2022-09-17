using Aeronaves.WebApi.Modelo;
using AutoMapper;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Aeronaves.WebApi.Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Aeronaves.WebApi.Aplicacion
{
    public class CambiarEstadoAeronave
    {
        public class CambiarEstado : IRequest<Aeronave>
        {
            public Guid AeronaveGuid { get; set; }
        }

        public class Manejador : IRequestHandler<CambiarEstado, Aeronave>
        {
            private readonly ContextoAeronave _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAeronave contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<Aeronave> Handle(CambiarEstado request, CancellationToken cancellationToken)
            {
                var Consulta = await _contexto.Aeronave.Where(x => x.AeronaveId == request.AeronaveGuid).FirstOrDefaultAsync();
                if (Consulta == null)
                {
                    throw new Exception("Estado Aeronave no Actualizada");
                }
                else
                {
                    Consulta.EstadoAeronave = "Operativo";
                    _contexto.SaveChanges();
                    return Consulta;
                }
            }
           
        }

    }
}
