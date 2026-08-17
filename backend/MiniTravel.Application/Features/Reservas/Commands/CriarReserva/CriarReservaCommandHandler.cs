using MiniTravel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Application.Features.Reservas.Commands.CriarReserva
{
    public class CriarReservaCommandHandler
    {
        private readonly IDestinoRepository _destinoRepository;
        private readonly IReservaRepository _reservaRepository;

        public CriarReservaCommandHandler(
            IDestinoRepository destinoRepository,
            IReservaRepository reservaRepository)
        {
            _destinoRepository = destinoRepository;
            _reservaRepository = reservaRepository;
        }

        public async Task<Reserva> HandleAsync(
            CriarReservaCommand command)
        {
            var destino = await _destinoRepository
                .ObterPorIdAsync(command.DestinoId);

            if (destino is null)
                throw new InvalidOperationException(
                    "Destino não encontrado.");

            var reserva = new Reserva(
                command.ClienteId,
                destino,
                command.DataViagem,
                command.QuantidadeViajantes);

            await _reservaRepository.AdicionarAsync(reserva);

            return reserva;
        }
    }
}
