using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Rabbitmq.Eventos;

namespace Shared.Rabbitmq.BusRabbit
{
    public interface IEventoManejador<in TEvent>: IEventoManejador where TEvent: Evento
    {
        Task Handle(TEvent @evento);
        
    }
    public interface IEventoManejador
    {


    }
}
