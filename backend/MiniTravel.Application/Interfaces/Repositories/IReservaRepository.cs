using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Domain.Entities;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Application.Interfaces.Repositories;

public interface IReservaRepository
{
    Task AdicionarAsync(Reserva reserva);
    Task<Reserva?> ObterPorIdAsync(Guid id);
    Task AtualizarAsync(Reserva reserva);
}
