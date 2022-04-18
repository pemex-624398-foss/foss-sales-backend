using AutoMapper;
using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Queries;

public static class GetCustomersByFilterQuery
{
    public record Argument(
        string? Name, 
        string? Email,
        string? Gender,
        string? City
        ) : IRequest<IEnumerable<CustomerResult>>;
        
    public class Handler : IRequestHandler<Argument, IEnumerable<CustomerResult>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        
        public Handler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResult>> Handle(Argument request, CancellationToken cancellationToken = default)
        {
            var (name, email, gender, city) = request;
            var customers = 
                await _customerRepository.GetByFilterAsync(name, email, gender, city);
            return customers.Select(c => _mapper.Map<CustomerResult>(c));
        }
    }
}