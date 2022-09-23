using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Aeronaves.WebApi.Persistencia;
using Aeronaves.WebApi.Modelo;
using System.Collections.Generic;
using Shared.Rabbitmq.BusRabbit;
using Shared.Rabbitmq.EventoQueue;

namespace Aeronaves.WebApi.Aplicacion
{
    public class CrearAeronave
    {
        public class Crear : IRequest<Guid>
        //public class Crear : IRequest
        {
            public Guid AeronaveId { get; set; }
            public string Marca { get; set; }
            public string Modelo { get; set; }
            public int NroAsientos { get; set; }
            public decimal CapacidadCarga { get; set; }
            public decimal CapTanqueCombustible { get; set; }
            public string AereopuertoEstacionamiento { get; set; }
            public string EstadoAeronave { get; set; } //(Operativo/Mantenimiento/Asignado) 
            public Guid AeronaveGuid { get; set; }
            
            public List<AeronaveAsientos> ListaAsientos { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Crear>
        {

            public EjecutaValidacion()
            {
                RuleFor(x => x.Marca).NotEmpty();
                RuleFor(x => x.Modelo).NotEmpty();
                RuleFor(x => x.NroAsientos).NotEmpty();
                RuleFor(x => x.CapacidadCarga).NotEmpty();
                RuleFor(x => x.CapTanqueCombustible).NotEmpty();
                RuleFor(x => x.AereopuertoEstacionamiento).NotEmpty();
                RuleFor(x => x.EstadoAeronave).NotEmpty();

            }
        }

        //public class Manejador : IRequestHandler<Crear>
        public class Manejador : IRequestHandler<Crear,Guid>
        {
            private readonly ContextoAeronave _contexto;

            /// <summary>
            /// Se implementa EventBus de RabbitMQ
            /// </summary>
            /// <param name="eventBus"></param>
            private readonly IRabbitEventBus _eventBus;

            
            public Manejador(ContextoAeronave contexto, IRabbitEventBus eventBus)
            {
                _contexto = contexto;
                _eventBus = eventBus;
            }
            //public async Task<Unit> Handle(Crear request, CancellationToken cancellationToken)
            public async Task<Guid> Handle(Crear request, CancellationToken cancellationToken)
            {

                var aeronave = new Aeronave
                {   
                    Marca = request.Marca,
                    Modelo = request.Modelo,
                    NroAsientos = request.NroAsientos,
                    CapacidadCarga = request.CapacidadCarga,
                    CapTanqueCombustible = request.CapTanqueCombustible,
                    AereopuertoEstacionamiento = request.AereopuertoEstacionamiento,
                    EstadoAeronave = request.EstadoAeronave,
                    AeronaveGuid = Guid.NewGuid()
                };

                _contexto.Aeronave.Add(aeronave);
                var value = await _contexto.SaveChangesAsync();

                              

                if (value == 0)
                {
                    throw new Exception("Error en el registro de Aeronave");
                }

                Guid id = aeronave.AeronaveId;

                /// Se publica en el bus de rabbit
                //_eventBus.Publish(new EmailEventoQueue("jyujra@bancosol.com.bo","Creacion de Aeronave " + request.Marca , "Se Creo la Aeronave y se notifica al bus de eventos"));                
                _eventBus.Publish(new AeronaveAgregadaEventoQueue(id, request.Marca, request.Modelo, request.NroAsientos, request.EstadoAeronave, "Se Creo la Aeronave y se notifica al bus de eventos"));
                //_eventBus.Publish(new VueloAsignadoAeronaveQueue(Guid.NewGuid(), Guid.NewGuid(),id));

                foreach (var obj in request.ListaAsientos)
                {
                    var detalleAsientos = new AeronaveAsientos
                    { 
                            AeronaveAsientosId = Guid.NewGuid(),
                            ClaseAsiento = obj.ClaseAsiento,
                            Ubicacion = obj.Ubicacion,
                            NroSilla = obj.NroSilla,
                            EstadoAsiento = obj.EstadoAsiento,
                            AeronaveId = id                           
                    };

                    _contexto.AeronaveAsientos.Add(detalleAsientos);
                }

                value = await _contexto.SaveChangesAsync();

                if (value > 0)
                {
                    //return Unit.Value;
                    return id;
                }

                throw new Exception("No se pudo insertar el detalle de Asientos");


            }
        }
    }
}
