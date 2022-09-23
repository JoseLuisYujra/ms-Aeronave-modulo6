using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Rabbitmq.Comandos;
using Shared.Rabbitmq.Eventos;

namespace Shared.Rabbitmq.BusRabbit
{
    public interface IRabbitEventBus
    {

        Task EnviarComando<T>(T comando) where T : Comando;
             
        void Publish<T>(T @evento) where T : Evento;
        void Subscribe<T, TH>() where T : Evento
                               where TH : IEventoManejador<T>;
                                              
    }
}
