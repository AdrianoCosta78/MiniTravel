using System;
using System.Collections.Generic;
using System.Text;

namespace MiniTravel.Application.Interfaces.Cache
{
    public interface ICacheService
    {
        Task<string?> ObterAsync(string chave);
        Task DefinirAsync(string chave, string valor, TimeSpan expiracao);
        Task RemoverAsync(string chave);
    }
}
