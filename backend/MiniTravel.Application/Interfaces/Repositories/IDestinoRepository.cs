using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Application.Interfaces.Repositories;

public interface IDestinoRepository
{
    Task<Destino?> ObterPorIdAsync(Guid id);
}
