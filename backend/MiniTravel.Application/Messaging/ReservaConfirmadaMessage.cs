using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTravel.Application.Messaging
{
    public class ReservaConfirmadaMessage
    {
        public Guid ReservaId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid DestinoId { get; set; }
        public DateTime DataViagem { get; set; }
    }
}
