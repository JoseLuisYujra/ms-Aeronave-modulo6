<h2 align="center">Proyecto Microservicio Aeronave - uso de RabbitMQ y deploy en Azure</h2>
<img src="https://img.shields.io/badge/Autor-Jose%20Yujra-blue" alt="autor"/>     
<h3 align="left">Lenguajes y Herramientas:</h3><p align="left"> 
<a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a><a href="https://www.docker.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/docker/docker-original-wordmark.svg" alt="docker" width="40" height="40"/> </a><a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> </a><a href="https://azure.microsoft.com/en-in/" target="_blank" rel="noreferrer"><img src="https://www.vectorlogo.zone/logos/microsoft_azure/microsoft_azure-icon.svg" alt="azure" width="40" height="40"/></a><a href="https://www.rabbitmq.com/" target="_blank" rel="noreferrer"><img src="https://www.vectorlogo.zone/logos/rabbitmq/rabbitmq-ar21.svg" alt="azure" width="90" height="90"/></a>
</p>

### Actividad
- Se utiliza los Nuggets
	- AutoMapper.Extensions.Microsoft.DependencyInjection
	- MediatR.Extensions.Microsoft.DependencyInjection
	- FluentValidation.AspNetCore
	- Microsoft.EntityFrameworkCore
	- Microsoft.EntityFrameworkCore.Design
	- Microsoft.EntityFrameworkCore.Tools
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.VisualStudio.Azure.Containers.Tools.Targets
	- Swashbuckle.AspNetCore

- Se realizo Despliegue: 
	- Base de datos en Azure :  srvdbaerolinea.database.windows.net
	- Despliegue de Microservicio Aeronave en Azure utilizando Contenedor Docker: https://aeronaveapi.azurewebsites.net/api/Aeronave
	- Despliegue de rRabbitMQ en azure: http://jyujrarabbitmqztiwdo5zlgpzo-vm0.southcentralus.cloudapp.azure.com:15672

- Se implementa el uso de RabbitMQ
- Se Desarrollo un Shared Kernel de Rabbit MQ para uso en todos los microservicios.

Configuracion relevantes para el **USO DE SHARED KERNEL (Shared.RabbitMQ)** :

##### appsettings.json
```json
{
  "ConnectionStrings": {    
    "ConexionDB": "Server=tcp:srvdbaerolinea.database.windows.net,1433;Initial Catalog=AeronaveDB;Persist Security Info=False;User ID=usrsql;Password=Password12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30"
  },

  "Services": {
    "APIAsignarAeronave": "http://localhost:34272/api/AsignarAeronave"
  },

  "RabbitMq": {
    "Host": "13.84.206.11",
    "Port": 5672,
    "UserName": "user",
    "Password": "xxxxxxxxx",
    "AeronaveCreadaExchanged": "AeronaveCreadaExchanged"
  }
}
```


##### Startup.cs
```c#
 services.AddTransient<IRabbitEventBus, RabbitEventBus>();
  services.AddTransient<IEventoManejador<VueloAsignadoAeronaveQueue>, AeronaveEventoManejador>();
  
   var eventBus = app.ApplicationServices.GetRequiredService<IRabbitEventBus>();
   eventBus.Subscribe<VueloAsignadoAeronaveQueue, AeronaveEventoManejador>();
```
##### Para PUBLICAR, Uso en la clase CrearAeronave.cs
```c#
....
private readonly IRabbitEventBus _eventBus;
public Manejador(ContextoAeronave contexto, IRabbitEventBus eventBus)
{
     _contexto = contexto;
     _eventBus = eventBus;
 }
 ....
_eventBus.Publish(new AeronaveAgregadaEventoQueue(id, request.Marca, request.Modelo, request.NroAsientos, request.EstadoAeronave, "Se Creo la Aeronave y se notifica al bus de eventos"));
```

##### Para CONSUMIR (Subscritor), Uso en la clase CrearAeronave.cs
incorporar el evento que consume ejemplo: VueloAsignadoAeronaveQueue

```c#
  ....
  var eventBus = app.ApplicationServices.GetRequiredService <IRabbitEventBus>();
  eventBus.Subscribe<VueloAsignadoAeronaveQueue, AeronaveEventoManejador>();
  ....
```