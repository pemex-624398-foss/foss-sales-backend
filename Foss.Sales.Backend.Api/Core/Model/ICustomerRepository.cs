namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public interface ICustomerRepository : IRepository<Customer, int>
{
    Task<IEnumerable<Customer>> GetByFilterAsync(string? name, string? email, string? gender, string? city);
    Task RenameAsync(int id, string firstName, string? lastName);
    Task UpdateEmailAsync(int id, string email);
}