using Aeronaves.WebApi.Aplicacion;
using Aeronaves.WebApi.Modelo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Aeronaves.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignarAeronaveController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AsignarAeronaveController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Aeronave>> AsignarAeronave(AsignarAeronave.Asignar data)
        {
            return await _mediator.Send(data);
        }
       
    }
}
