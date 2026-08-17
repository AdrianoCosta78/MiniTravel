using System;
using System.Collections.Generic;
using System.Text;
using MiniTravel.Application.Interfaces.Cache;
using StackExchange.Redis;

namespace MiniTravel.Infrastructure.Cache;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<string?> ObterAsync(string chave)
    {
        var valor = await _database.StringGetAsync(chave);

        return valor.HasValue
            ? valor.ToString()
            : null;
    }

    public async Task DefinirAsync(
        string chave,
        string valor,
        TimeSpan expiracao)
    {
        var resultado = await _database.StringSetAsync(
        chave,
        valor,
        expiracao);

        Console.WriteLine(
            $"Redis SET: {chave} - resultado: {resultado}");
    }

    public async Task RemoverAsync(string chave)
    {
        await _database.KeyDeleteAsync(chave);

    }
}
