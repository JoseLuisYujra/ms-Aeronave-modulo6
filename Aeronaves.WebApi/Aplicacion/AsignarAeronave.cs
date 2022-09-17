using Aeronaves.WebApi.Modelo;
using MediatR;
using System.Collections.Generic;
using System;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Aeronaves.WebApi.Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Aeronaves.WebApi.Aplicacion
{
    public class AsignarAeronave
    {

        public class Asignar : IRequest<Aeronave>
        {
            public Guid AeronaveGuid { get; set; }
                      
        }

        public class Manejador : IRequestHandler<Asignar, Aeronave>
        {
            private readonly ContextoAeronave _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAeronave contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }
                        
            public async Task<Aeronave> Handle(Asignar request, CancellationToken cancellationToken)
            {
                var Consulta = await _contexto.Aeronave.Where(x => x.AeronaveId == request.AeronaveGuid).FirstOrDefaultAsync();
                if (Consulta == null)
                {
                    throw new Exception("Aeronave Ya asignada");
                }
                else
                {
                    Consulta.EstadoAeronave = "Asignado";
                    _contexto.SaveChanges();
                    return Consulta;
                }
            }          
           
        }

    }
}
