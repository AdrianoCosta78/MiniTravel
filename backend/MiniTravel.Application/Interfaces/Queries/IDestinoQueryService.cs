using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Application.Features.Destinos.Queries.ListarDestinos;

namespace MiniTravel.Application.Interfaces.Queries;

public interface IDestinoQueryService
{
    Task<IEnumerable<DestinoDTO>> ListarAtivosAsync();
}
