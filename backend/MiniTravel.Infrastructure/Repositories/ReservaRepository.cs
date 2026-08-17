using Microsoft.EntityFrameworkCore;
using MiniTravel.Application.Interfaces.Repositories;
using MiniTravel.Domain.Entities;
using MiniTravel.Infrastructure.Persistence;

namespace MiniTravel.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly MiniTravelDbContext _context;

        public ReservaRepository(MiniTravelDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<Reserva?> ObterPorIdAsync(Guid id)
        {
            return await _context.Reservas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AtualizarAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();
        }
    }
}
