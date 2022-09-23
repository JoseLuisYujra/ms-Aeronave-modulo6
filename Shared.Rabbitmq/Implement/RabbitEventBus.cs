using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Rabbitmq.BusRabbit;
using Shared.Rabbitmq.Comandos;
using Shared.Rabbitmq.Eventos;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace Shared.Rabbitmq.Implement
{
   
    public class RabbitEventBus: IRabbitEventBus
    {
        private readonly IMediator _mediator;       
        private readonly Dictionary<string, List<Type>> _manejadores;
        private readonly List<Type> _eventoTipos;

        private readonly IConfiguration _configuration;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitEventBus(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _manejadores = new Dictionary<string, List<Type>>();
            _eventoTipos = new List<Type>();
            _configuration = configuration;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMq:Host"],
                UserName = _configuration["RabbitMq:UserName"],
                Password = _configuration["RabbitMq:Password"],                
                Port = Int32.Parse(_configuration["RabbitMq:Port"])                
            };
        }

        public Task EnviarComando<T>(T comando) where T : Comando
        {
            return _mediator.Send(comando);
        }

        public void Publish<T>(T evento) where T : Evento
        {
            using IConnection connection = _connectionFactory.CreateConnection();            
            
            using (var channel = connection.CreateModel())
                            
                { var eventName = evento.GetType().Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var message = JsonConvert.SerializeObject(evento);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", eventName,null, body);
            }
        }

      

        public void Subscribe<T, TH>()
            where T : Evento
            where TH : IEventoManejador<T>
        {
            var rabbitMqHost = _configuration["RabbitMq:Host"];            
            var rabbitMqUserName = _configuration["RabbitMq:UserName"];
            var rabbitMqPassword = _configuration["RabbitMq:Password"];
            //var rabbitMqPort = _configuration["RabbitMq:Port"];

            var eventoNombre = typeof(T).Name;
            var manejadorEventoTipo = typeof(TH);

            if (!_eventoTipos.Contains(typeof(T)))
            {
                _eventoTipos.Add(typeof(T));
            }
            if (!_manejadores.ContainsKey(eventoNombre))
            {
                _manejadores.Add(eventoNombre, new List<Type>());
            }
            if (_manejadores[eventoNombre].Any(x => x.GetType() == manejadorEventoTipo))
            {
                throw new ArgumentException($"El manejador {manejadorEventoTipo.Name} fue registrado anteriormente por {eventoNombre}");
            }
            _manejadores[eventoNombre].Add(manejadorEventoTipo);
                     

            var factory = new ConnectionFactory()
            {
                HostName = rabbitMqHost,
                UserName = rabbitMqUserName,
                Password = rabbitMqPassword,
                //Port = Int32.Parse(rabbitMqPort),
                DispatchConsumersAsync = true
            };


            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(eventoNombre, false, false, false, null);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Delegate;
            channel.BasicConsume(eventoNombre, true, consumer);
        }

        private async Task Consumer_Delegate(object sender, BasicDeliverEventArgs e)
        {
            var nombreEvento = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            try
            {
                if(_manejadores.ContainsKey(nombreEvento))
                {
                    var subscriptions = _manejadores[nombreEvento];
                        foreach (var sb in subscriptions) {
                        var manejador = Activator.CreateInstance(sb);
                        if (manejador == null) continue;
                        var tipoEvento = _eventoTipos.SingleOrDefault(x => x.Name == nombreEvento);
                        var eventoDS = JsonConvert.DeserializeObject(message, tipoEvento);
                        var concretoTipo = typeof(IEventoManejador<>).MakeGenericType(tipoEvento);
                        await (Task)concretoTipo.GetMethod("Handle").Invoke(manejador, new object[] { eventoDS });
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    } 
}
