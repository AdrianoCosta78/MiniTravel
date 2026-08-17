using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using MiniTravel.Application.Interfaces.Cache;
using MiniTravel.Application.Interfaces.Queries;

namespace MiniTravel.Application.Features.Destinos.Queries.ListarDestinos
{
    public class ListarDestinosQueryHandler
    {
        private readonly IDestinoQueryService _destinoQueryService;
        private readonly ICacheService _cacheService;

        private const string CacheKey = "destinos:ativos";

        public ListarDestinosQueryHandler(
            IDestinoQueryService destinoQueryService,
            ICacheService cacheService)
        {
            _destinoQueryService = destinoQueryService;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<DestinoDTO>> HandleAsync(
            ListarDestinosQuery query)
        {
            var cache = await _cacheService.ObterAsync(CacheKey);

            if (!string.IsNullOrWhiteSpace(cache))
            {
                Console.WriteLine("CACHE HIT - destinos recuperados do Redis");

                var destinosCache =
                    JsonSerializer.Deserialize<IEnumerable<DestinoDTO>>(cache);

                return destinosCache ?? Enumerable.Empty<DestinoDTO>();
            }

            Console.WriteLine("CACHE MISS - buscando destinos no SQL Server");

            var destinos =
                await _destinoQueryService.ListarAtivosAsync();

            var json = JsonSerializer.Serialize(destinos);

            await _cacheService.DefinirAsync(
                CacheKey,
                json,
                TimeSpan.FromMinutes(5));

            return destinos;
        }
    }
}