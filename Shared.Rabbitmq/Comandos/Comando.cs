using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Rabbitmq.Eventos;

namespace Shared.Rabbitmq.Comandos
{
    public abstract class Comando : Message
    {
        public DateTime Timestamp { get;protected set; }

        protected Comando()
        {
            Timestamp = DateTime.Now;   
        }
    }
}
