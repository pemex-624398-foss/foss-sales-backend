namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public interface IRepository<TEntity, in TIdentity> where TEntity : class
{
    public string TableName { get; }
    public string KeyName { get; }
    Task<TEntity?> GetByIdAsync(TIdentity id);
    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteByIdAsync(TIdentity identity);
}