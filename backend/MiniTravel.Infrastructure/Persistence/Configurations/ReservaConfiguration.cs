using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Infrastructure.Persistence.Configurations;

public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
{
    public void Configure(EntityTypeBuilder<Reserva> builder)
    {
        builder.ToTable("Reservas");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteId)
            .IsRequired();

        builder.Property(x => x.DestinoId)
            .IsRequired();

        builder.Property(x => x.DataViagem)
            .IsRequired();

        builder.Property(x => x.QuantidadeViajantes)
            .IsRequired();

        builder.Property(x => x.ValorTotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();
    }
}
