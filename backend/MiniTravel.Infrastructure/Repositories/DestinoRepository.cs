using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Domain.Entities;
using MiniTravel.Infrastructure.Persistence;

namespace MiniTravel.Infrastructure.Repositories;

public class DestinoRepository : IDestinoRepository
{
    private readonly MiniTravelDbContext _context;

    public DestinoRepository(MiniTravelDbContext context)
    {
        _context = context;
    }

    public async Task<Destino?> ObterPorIdAsync(Guid id)
    {
        return await _context.Destinos
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
