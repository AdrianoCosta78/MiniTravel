using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniTravel.Application.Features.Destinos.Queries.ListarDestinos;

namespace MiniTravel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DestinosController : ControllerBase
    {
        private readonly ListarDestinosQueryHandler _handler;

        public DestinosController(ListarDestinosQueryHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var query = new ListarDestinosQuery();

            var destinos = await _handler.HandleAsync(query);

            return Ok(destinos);
        }
    }
}

