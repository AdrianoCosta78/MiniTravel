using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTravel.Application.Features.Destinos.Queries.ListarDestinos
{
    public class DestinoDTO
    {
        public Guid Id { get; set; }
        public string Cidade { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
    }
}
