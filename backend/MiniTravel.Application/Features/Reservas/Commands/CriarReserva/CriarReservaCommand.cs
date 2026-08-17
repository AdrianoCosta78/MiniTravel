using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTravel.Application.Features.Reservas.Commands.CriarReserva
{
    public class CriarReservaCommand
    {
        public Guid ClienteId { get; set; }
        public Guid DestinoId { get; set; }
        public DateTime DataViagem { get; set; }
        public int QuantidadeViajantes { get; set; }
    }
}
