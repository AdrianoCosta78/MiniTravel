using Microsoft.AspNetCore.Mvc;
using MiniTravel.Application.Features.Reservas.Commands.ConfirmarReserva;
using MiniTravel.Application.Features.Reservas.Commands.CriarReserva;

namespace MiniTravel.API.Controllers
{
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly CriarReservaCommandHandler _criarHandler;
        private readonly ConfirmarReservaCommandHandler _confirmarHandler;

        public ReservasController(
               CriarReservaCommandHandler criarHandler,
               ConfirmarReservaCommandHandler confirmarHandler)
        {
            _criarHandler = criarHandler;
            _confirmarHandler = confirmarHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(
            [FromBody] CriarReservaCommand command)
        {
            var reserva = await _criarHandler.HandleAsync(command);

            return Ok(reserva);
        }

        [HttpPut("{id:guid}/confirmar")]
        public async Task<IActionResult> Confirmar(Guid id)
        {
            var command = new ConfirmarReservaCommand
            {
                ReservaId = id
            };

            await _confirmarHandler.HandleAsync(command);

            return NoContent();
        }
    }
}
