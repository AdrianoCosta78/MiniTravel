using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MiniTravel.Application.Features.Destinos.Queries.ListarDestinos;
using MiniTravel.Application.Interfaces.Queries;

namespace MiniTravel.Infrastructure.Queries
{
    public class DestinoQueryService : IDestinoQueryService
    {
        private readonly string _connectionString;

        public DestinoQueryService(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Connection string não encontrada.");
        }

        public async Task<IEnumerable<DestinoDTO>> ListarAtivosAsync()
        {
            const string sql = """
            SELECT
                Id,
                Cidade,
                Pais,
                PrecoBase
            FROM Destinos
            WHERE Ativo = 1
            ORDER BY Cidade;
            """;

            await using var connection =
                new SqlConnection(_connectionString);

            return await connection.QueryAsync<DestinoDTO>(sql);
        }
    }
}
