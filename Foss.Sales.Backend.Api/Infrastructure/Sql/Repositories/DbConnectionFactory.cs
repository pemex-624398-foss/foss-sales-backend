using System.Data;
using System.Data.Common;
using Npgsql;

namespace Pemex.Foss.Sales.Backend.Api.Infrastructure.Sql.Repositories;

public class DbConnectionFactory : IDbConnectionFactory
{
    public const string NpgsqlProvider = "Npgsql";
    
    public DbConnectionFactory(string key, string providerName, string connectionString)
    {
        DbProviderFactories.RegisterFactory(NpgsqlProvider, NpgsqlFactory.Instance);
        
        Key = key;
        ProviderName = providerName;
        ConnectionString = connectionString;
    }

    public string Key { get; }
    public string ProviderName { get; }
    public string ConnectionString { get; }
    
    public IDbConnection GetConnection()
    {
        var providerFactory = DbProviderFactories.GetFactory(ProviderName);
        var connection = providerFactory.CreateConnection() ?? throw new InvalidOperationException();
        connection.ConnectionString = ConnectionString;
        return connection;
    }
}