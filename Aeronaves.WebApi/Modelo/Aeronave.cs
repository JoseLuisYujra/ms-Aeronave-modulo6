using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeronaves.WebApi.Modelo
{
    public class Aeronave
    {

        public Guid AeronaveId { get;  set; }        
        public string Marca { get;  set; }
        public string Modelo { get;  set; }
        public int NroAsientos { get;  set; }
        public decimal CapacidadCarga { get;  set; }
        public decimal CapTanqueCombustible { get;  set; }
        public string AereopuertoEstacionamiento { get;  set; }
        public string EstadoAeronave { get;  set; } //(Operativo/Mantenimiento/Asignado) 
        public Guid AeronaveGuid { get; set; }

        [NotMapped]
        public ICollection<AeronaveAsientos> ListaAeronaveAsientos { get; set; }        

    }
}
