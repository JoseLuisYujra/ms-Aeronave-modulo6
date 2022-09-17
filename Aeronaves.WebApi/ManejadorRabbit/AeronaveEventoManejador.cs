using Aeronaves.WebApi.Controllers;
using Aeronaves.WebApi.Modelo;
using Aeronaves.WebApi.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Rabbitmq.BusRabbit;
using Shared.Rabbitmq.EventoQueue;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aeronaves.WebApi.ManejadorRabbit
{
    public class AeronaveEventoManejador : IEventoManejador<VueloAsignadoAeronaveQueue>
    {
        /// <summary>
        /// Clase para el CONSUMIDOR uso de RabbitMQ
        /// </summary>    
        
        /*
        //private readonly ILogger<VueloAsignadoAeronaveQueue> _log;    
        private readonly IConfiguration _configuration;

        public AeronaveEventoManejador(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        */
        //public AeronaveEventoManejador() { }


        //public EmailEventoManejador(ILogger<AeronaveEventoManejador> loggers)
        //{
        //    _logger = loggers;
        //}
        public Task Handle(VueloAsignadoAeronaveQueue @evento)        
        {

            var url = "http://localhost:34272/api/AsignarAeronave";            
            //var url = _configuration["Services:APIAsignarAeronave"];            
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Timeout = 300000;
            var json = JsonConvert.SerializeObject(evento);

            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(json);

            Stream newStream = request.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            // Invocación del servicio y respuesta
            var response = request.GetResponse();

            return Task.CompletedTask;

        }
    }
}
