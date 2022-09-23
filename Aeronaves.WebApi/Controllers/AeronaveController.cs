using Aeronaves.WebApi.Aplicacion;
using Aeronaves.WebApi.Aplicacion.Dto;
using Aeronaves.WebApi.Aplicacion.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Aeronaves.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronaveController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AeronaveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost]
        //public async Task<ActionResult<Unit>> Crear(CrearAeronave.Crear data)        
        public async Task<ActionResult> Crear(CrearAeronave.Crear data)
        {
            //return await _mediator.Send(data);
            Guid id = await _mediator.Send(data);
            return Ok(id);
        }    
        
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<AeronaveDto>>> GetAllAeronaves()
        {
            return await _mediator.Send(new Consulta.ListaAeronave());
        }

        
        [Route("{id:guid}")]
        [HttpGet]
        //[HttpGet("{id}")]
        public async Task<ActionResult<AeronaveDto>> GetAeronave(Guid id)
        {
            return await _mediator.Send(new ConsultaFiltro.AeronveUnico { AeronaveGuid = id });
        }
    }
}
