using MiniTravel.Application.Features.Reservas.Commands.ConfirmarReserva;
using MiniTravel.Application.Interfaces.Messaging;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Application.Messaging;

namespace MiniTravel.Application.Features.Reservas.Commands.ConfirmarReserva;

public class ConfirmarReservaCommandHandler
{
    private readonly IReservaRepository _reservaRepository;
    private readonly IMessageBus _messageBus;

    public ConfirmarReservaCommandHandler(
        IReservaRepository reservaRepository,
        IMessageBus messageBus)
    {
        _reservaRepository = reservaRepository;
        _messageBus = messageBus;
    }

    public async Task HandleAsync(
        ConfirmarReservaCommand command)
    {
        var reserva = await _reservaRepository
            .ObterPorIdAsync(command.ReservaId);

        if (reserva is null)
            throw new InvalidOperationException(
                "Reserva não encontrada.");

        reserva.Confirmar();

        await _reservaRepository
            .AtualizarAsync(reserva);

        var message = new ReservaConfirmadaMessage
        {
            ReservaId = reserva.Id,
            ClienteId = reserva.ClienteId,
            DestinoId = reserva.DestinoId,
            DataViagem = reserva.DataViagem
        };

        await _messageBus
            .PublicarReservaConfirmadaAsync(message);
    }
}
