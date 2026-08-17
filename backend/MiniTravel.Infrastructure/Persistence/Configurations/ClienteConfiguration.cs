using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using MiniTravel.Domain.Entities;

namespace MiniTravel.Infrastructure.Persistence.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");
    
        builder.HasKey(x => x.Id);
    
        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);
    
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);
    
        builder.Property(x => x.Documento)
            .IsRequired()
            .HasMaxLength(20);
    }
}
