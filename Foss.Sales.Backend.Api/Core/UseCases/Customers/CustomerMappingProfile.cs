using AutoMapper;
using Pemex.Foss.Sales.Backend.Api.Core.Model;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Queries;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerResult>();
        CreateMap<CreateCustomerCommand.Argument, Customer>();
    }
}