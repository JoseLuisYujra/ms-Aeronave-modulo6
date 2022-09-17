using Shared.Rabbitmq.BusRabbit;
using Shared.Rabbitmq.EventoQueue;
using System.Threading.Tasks;

namespace Aeronaves.WebApi.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventoQueue>
    {
        //private readonly ILogger<EmailEventoManejador> _logger;  

        public EmailEventoManejador() { }

        //public EmailEventoManejador(ILogger<EmailEventoManejador> loggers)
        //{
        //    _logger = loggers;
        //}


        public Task Handle(EmailEventoQueue @evento)
        {
           // _logger.LogInformation($"Este es el Valor que consumo desde RabbitMQ {@evento.Titulo}");




            return Task.CompletedTask;
        }
    }
}
