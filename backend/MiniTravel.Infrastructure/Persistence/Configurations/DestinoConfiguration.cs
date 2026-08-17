using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Infrastructure.Persistence.Configurations;

public class destinoConfiguration : IEntityTypeConfiguration<Destino>
{
    public void Configure(EntityTypeBuilder<Destino> builder)
    {
        builder.ToTable("Destinos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Cidade)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Pais)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PrecoBase)
            .HasPrecision(18, 2);

        builder.Property(x => x.Ativo)
            .IsRequired();
    }
}
