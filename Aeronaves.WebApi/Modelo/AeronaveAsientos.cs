using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aeronaves.WebApi.Modelo
{
    public class AeronaveAsientos
    {

        public Guid AeronaveAsientosId { get; set; }     
        public string ClaseAsiento { get; set; } //(Economica(20)/Ejecutiva(10)) -> enum
        public string Ubicacion { get; set; }    //(Ventana,Central,Pasillo)  -> enum
        public int NroSilla { get; set; }
        public string EstadoAsiento { get; set; }  //(Reservado/Libre)

        public Guid AeronaveId { get; set; }
        
        [NotMapped]
        public Aeronave Aeronave { get; set; }       

    }
}
