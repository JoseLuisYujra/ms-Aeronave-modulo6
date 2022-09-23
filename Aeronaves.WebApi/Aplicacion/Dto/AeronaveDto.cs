using Aeronaves.WebApi.Modelo;
using System.Collections.Generic;
using System;

namespace Aeronaves.WebApi.Aplicacion.Dto
{
    public class AeronaveDto
    {

        public Guid AeronaveId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int NroAsientos { get; set; }
        public decimal CapacidadCarga { get; set; }
        public decimal CapTanqueCombustible { get; set; }
        public string AereopuertoEstacionamiento { get; set; }
        public string EstadoAeronave { get; set; }                   
        
    }
}
