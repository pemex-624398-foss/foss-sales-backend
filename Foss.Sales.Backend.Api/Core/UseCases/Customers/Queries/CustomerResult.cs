namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Queries;

public record CustomerResult(
    int Id,
    string FirstName,
    string? LastName,
    string? Email,
    string? Gender,
    string City
    );