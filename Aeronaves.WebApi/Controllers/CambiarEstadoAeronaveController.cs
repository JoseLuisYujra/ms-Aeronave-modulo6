using Aeronaves.WebApi.Aplicacion;
using Aeronaves.WebApi.Modelo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aeronaves.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambiarEstadoAeronaveController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CambiarEstadoAeronaveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Aeronave>> AsignarAeronave(CambiarEstadoAeronave.CambiarEstado data)
        {
            return await _mediator.Send(data);
        }

    }


}
