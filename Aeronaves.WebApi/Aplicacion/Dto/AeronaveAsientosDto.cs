using System;

namespace Aeronaves.WebApi.Aplicacion.Dto
{
    public class AeronaveAsientosDto
    {
        public Guid AeronaveAsientosId { get; set; }
        public string ClaseAsiento { get; set; }
        public string Ubicacion { get; set; }   
        public int NroSilla { get; set; }
        public string EstadoAsiento { get; set; }

        public Guid AeronaveId { get; set; }
    }
}
