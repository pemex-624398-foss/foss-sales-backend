using AutoMapper;
using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

public static class CreateCustomerCommand
{
    public record Argument(
        string FirstName,
        string? LastName,
        string? Email,
        string? Gender,
        string City
        ) : IRequest<Result>;

    public class Handler : IRequestHandler<Argument, Result>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        
        public Handler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(Argument request, CancellationToken cancellationToken = default)
        {
            var customer = _mapper.Map<Customer>(request);
            await _customerRepository.InsertAsync(customer);
            return new Result(customer.Id);
        }
    }

    public record Result(int CustomerId);
}