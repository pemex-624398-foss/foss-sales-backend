using System.Data;
using CaseExtensions;
using Dapper;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Infrastructure.Sql.Repositories;

public class SqlRepository<TEntity, TIdentity> : IRepository<TEntity, TIdentity> where TEntity : class
{
    protected SqlRepository(IDbConnectionFactory connectionFactory)
    {
        ConnectionFactory = connectionFactory;
    }

    protected SqlRepository(IDbTransaction transaction)
    {
        Transaction = transaction;
    }

    protected IDbConnectionFactory? ConnectionFactory { get; }
    protected IDbTransaction? Transaction { get; }

    public virtual string TableName => typeof(TEntity).Name.ToSnakeCase();
    public virtual string KeyName => "id";

    protected IDbConnection GetConnection() =>
        Transaction?.Connection
        ?? ConnectionFactory?.GetConnection()
        ?? throw new InvalidOperationException();

    protected Task<IEnumerable<TEntity>> QueryAsync(
        string sql,
        IReadOnlyDictionary<string, object>? param = default,
        CommandType commandType = CommandType.Text
        )
    {
        return QueryAsync(
            sql,
            param != default ? new DynamicParameters(param) : default,
            commandType
            );
    }

    protected async Task<IEnumerable<TEntity>> QueryAsync(
        string sql,
        DynamicParameters? param = default,
        CommandType commandType = CommandType.Text
        )
    {
        using var connection = GetConnection();
        return await connection.QueryAsync<TEntity>(sql, param, commandType: commandType);
    }

    protected Task<TEntity?> QueryFirstOrDefaultAsync(
        string sql,
        IReadOnlyDictionary<string, object>? param = default,
        CommandType commandType = CommandType.Text
        )
    {
        return QueryFirstOrDefaultAsync(
            sql,
            param != default ? new DynamicParameters(param) : default,
            commandType
            );
    }

    protected async Task<TEntity?> QueryFirstOrDefaultAsync(
        string sql,
        DynamicParameters? param = default,
        CommandType commandType = CommandType.Text
    )
    {
        using var connection = GetConnection();
        return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, commandType: commandType);
    }

    protected Task<int> ExecuteAsync(
        string sql,
        IReadOnlyDictionary<string, object>? param = default,
        CommandType commandType = CommandType.Text
        )
    {
        return ExecuteAsync(
            sql,
            param != default ? new DynamicParameters(param) : default,
            commandType
            );
    }

    protected async Task<int> ExecuteAsync(
        string sql,
        DynamicParameters? param = default,
        CommandType commandType = CommandType.Text
        )
    {
        using var connection = GetConnection();
        return await connection.ExecuteAsync(sql, param, commandType: commandType);
    }

    public virtual Task<TEntity?> GetByIdAsync(TIdentity id)
    {
        throw new NotSupportedException();
    }

    public virtual Task InsertAsync(TEntity entity)
    {
        throw new NotSupportedException();
    }

    public virtual Task UpdateAsync(TEntity entity)
    {
        throw new NotSupportedException();
    }

    public virtual async Task DeleteByIdAsync(TIdentity id)
    {
        var affected = await ExecuteAsync(
            $"delete from {TableName} where {KeyName} = @Id",
            new DynamicParameters(new
            {
                Id = id
            })
            );

        if (affected == 0)
            throw new EntityNotFoundException($"Customer not found for id {id}", id!);

        if (affected != 1)
            throw new InvalidOperationException("Multiple records affected.");
    }
}
