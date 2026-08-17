using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Infrastructure.Persistence;

public class MiniTravelDbContext : DbContext
{
    public MiniTravelDbContext(DbContextOptions<MiniTravelDbContext> options) : base(options) 
    {
    }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Destino> Destinos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(MiniTravelDbContext).Assembly);
    }
}
