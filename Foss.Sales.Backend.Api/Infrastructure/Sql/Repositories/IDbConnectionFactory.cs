using System.Data;

namespace Pemex.Foss.Sales.Backend.Api.Infrastructure.Sql.Repositories;

public interface IDbConnectionFactory
{
    string ProviderName { get; }
    string ConnectionString { get; }
    IDbConnection GetConnection();
}