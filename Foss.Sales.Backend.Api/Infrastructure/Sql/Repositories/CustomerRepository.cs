using System.Data;
using Dapper;
using Pemex.Foss.Sales.Backend.Api.Core.Model;
using Serilog;

namespace Pemex.Foss.Sales.Backend.Api.Infrastructure.Sql.Repositories;

public class CustomerRepository : SqlRepository<Customer, int>, ICustomerRepository
{
    public CustomerRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public CustomerRepository(IDbTransaction transaction) : base(transaction)
    {
    }

    public override Task<Customer?> GetByIdAsync(int id)
    {
        return QueryFirstOrDefaultAsync(
            @"
                    select id as Id,
                           first_name as FirstName, 
                           last_name as LastName, 
                           email as Email, 
                           gender as Gender, 
                           city as City 
                    from customer
                    where id = @Id
                   ", 
            new DynamicParameters(new
            {
                Id = id
            })
        );
    }
    
    public Task<IEnumerable<Customer>> GetByFilterAsync(string? name, string? email, string? gender, string? city)
    {
        return QueryAsync(
            @"
                    select id as Id,
                           first_name as FirstName, 
                           last_name as LastName, 
                           email as Email, 
                           gender as Gender, 
                           city as City 
                    from customer
                    where 
                          (
                              lower(trim(first_name)) like '%' || coalesce(@Name, '') || '%' or
                              lower(trim(last_name)) like '%' || coalesce(@Name, '') || '%'
                          ) and
                          lower(trim(email)) like '%' || coalesce(@Email, '') || '%' and
                          (lower(trim(gender)) = @Gender or @Gender is null) and
                          lower(trim(city)) like '%' || coalesce(@City, '') || '%'
                   ", 
            new DynamicParameters(new
            {
                Name = name?.Trim().ToLower(),
                Email = email?.Trim().ToLower(),
                Gender = gender?.Trim().ToLower(),
                City = city?.Trim().ToLower()
            })
        );
    }
    
    public override async Task InsertAsync(Customer entity)
    {
        using var connection = GetConnection();
        var newRecord = await connection.QueryFirstOrDefaultAsync<dynamic>(
            @"
            insert into customer (first_name, last_name, email, gender, city)
            values (@FirstName, @LastName, @Email, @Gender, @City) 
            returning (id)
            ",
            entity
        );
        entity.Id = newRecord.id;
    }

    public async Task RenameAsync(int id, string firstName, string? lastName)
    {
        var affected = await ExecuteAsync(
            "update customer set first_name = @FirstName, last_name = @LastName where id = @Id",
            new DynamicParameters(new
            {
                FirstName = firstName.Trim(), 
                LastName = lastName?.Trim(),
                Id = id
            })
            );
        
        if (affected == 0)
            throw new EntityNotFoundException($"Customer not found for id {id}.", id!);

        if (affected != 1)
            throw new MultipleEntityAffectedException("Multiple customers affected.");
    }

    public async Task UpdateEmailAsync(int id, string email)
    {
        var affected = await ExecuteAsync(
            "update customer set email = @Email where id = @Id",
            new DynamicParameters(new
            {
                Email = email.Trim(),
                Id = id
            })
            );
        
        if (affected == 0)
            throw new EntityNotFoundException($"Customer not found for id {id}.", id!);

        if (affected != 1)
            throw new MultipleEntityAffectedException("Multiple customers affected.");
    }
}