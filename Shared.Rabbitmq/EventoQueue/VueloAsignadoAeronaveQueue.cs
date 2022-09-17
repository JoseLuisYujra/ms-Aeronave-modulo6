using Shared.Rabbitmq.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Rabbitmq.EventoQueue
{
    public class VueloAsignadoAeronaveQueue : Evento
    {

        public Guid VueloGuid { get; set; }
        public Guid TripulacionGuid { get; set; }                
        public Guid AeronaveGuid { get; set; }

        public VueloAsignadoAeronaveQueue(Guid vueloguid, Guid tripulacionGuid, Guid aeronaveGuid)
        {
            VueloGuid = vueloguid;
            TripulacionGuid = tripulacionGuid;            
            AeronaveGuid = aeronaveGuid;
        }
    }
}
