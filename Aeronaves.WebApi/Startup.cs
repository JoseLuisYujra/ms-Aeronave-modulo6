using Aeronaves.WebApi.Aplicacion;
using Aeronaves.WebApi.Aplicacion.UseCases.Queries;
using Aeronaves.WebApi.ManejadorRabbit;
using Aeronaves.WebApi.Persistencia;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shared.Rabbitmq.BusRabbit;
using Shared.Rabbitmq.EventoQueue;
using Shared.Rabbitmq.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeronaves.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            ///<Sumary>
            ///Se agrega el bus de RabbitMQ, sera utiliza para cualquier microservicio.
            ///Para el PRODUCTOR (Publisher) y tambien para el consumidor
            ///<Sumary>
            services.AddTransient<IRabbitEventBus, RabbitEventBus>();

            ///<Sumary>
            ///Se agrega el eventomanejador de RabbitMQ.
            ///Para el CONSUMIDOR (Subscriber), implementacion de evento manejador
            ///<Sumary>                        
            
//          services.AddTransient<IEventoManejador<EmailEventoQueue>, EmailEventoManejador>();
//          services.AddTransient<IEventoManejador<AeronaveAgregadaEventoQueue>, AeronaveEventoManejador>();

            services.AddTransient<IEventoManejador<VueloAsignadoAeronaveQueue>, AeronaveEventoManejador>();
        
            ///<Sumary>
            ///agregando en el controles la validacion
            ///<Sumary>
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CrearAeronave>());

            services.AddDbContext<ContextoAeronave>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ConexionDB"));
            });
                      
            ///<Sumary>
            ///agregando mediatR
            ///<Sumary>
            services.AddMediatR(typeof(CrearAeronave.Manejador).Assembly);

            ///<Sumary>
            ///agregando automapper para los DTOs
            ///<Sumary>
            services.AddAutoMapper(typeof(Consulta.Manejador));

            ///<Sumary>
            ///agregando Swagger
            ///<Sumary>           
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aeronaves.webApi", Version = "v1" });
            });
            */

            services.AddSwaggerGen();

            //services.AddInfrastructure(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(option => option.SwaggerEndpoint("/swagger/v1/swagger.json", "Aeronaves.WebApi v1"));          
           

            app.UseRouting();

            app.UseAuthorization();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ///<Sumary>
            ///registrar el evento Queue del bus
            ///Para el CONSUMIDOR (Subscriber) al evento ejemplo EmailEventoQueue que esta en el bus
            ///<Sumary>
            var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
            //eventBus.Subscribe<EmailEventoQueue, EmailEventoManejador>();
            //eventBus.Subscribe<AeronaveAgregadaEventoQueue, AeronaveEventoManejador>();
            eventBus.Subscribe<VueloAsignadoAeronaveQueue, AeronaveEventoManejador>();
            

        }
    }
}
