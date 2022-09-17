using Aeronaves.WebApi.Aplicacion.UseCases.Queries;
using Aeronaves.WebApi.Persistencia;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Rabbitmq.EventoQueue;
using System.Linq;
using System.Threading.Tasks;

namespace Aeronaves.WebApi.ManejadorRabbit
{
    public class AeronaveCreadaConsumer : IConsumer<AeronaveAgregadaEventoQueue>
    {
            
        public const string ExchangeName = "Aeronave-creada-exchange";
        public const string QueueName = "AeronaveAgregadaEventoQueue";

        private readonly ILogger<AeronaveAgregadaEventoQueue> _log;
        private readonly ContextoAeronave _contexto;
        private readonly IMediator _mediator;

        public AeronaveCreadaConsumer(ILogger<AeronaveAgregadaEventoQueue> log, ContextoAeronave context, IMediator mediator)
        {
            _log = log;
            _contexto = context;
            _mediator = mediator;
        }


        public async Task Consume(ConsumeContext<AeronaveAgregadaEventoQueue> context)
        {

            _log.LogInformation("Nuevo evento: Aeronave Estado Actualizada {0}.", context.Message.AeronaveGuid);
            _log.LogWarning("Estado Aeronave: {0}", context.Message.EstadoAeronave);

            ///<summary>
            ///Actualizar estado de Aeronave
            ///</summary>
            var aeronave = await _contexto.Aeronave.Where(sq => sq.AeronaveId == context.Message.AeronaveGuid).FirstOrDefaultAsync();


            aeronave.EstadoAeronave = "Asignado";
            _contexto.SaveChanges();

            _log.LogWarning("Estado de Aeronave Actualizada");
            
            await _contexto.SaveChangesAsync();
        }
    }
}
