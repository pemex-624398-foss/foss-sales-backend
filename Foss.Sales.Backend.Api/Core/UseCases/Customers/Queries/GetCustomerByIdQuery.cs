using AutoMapper;
using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Queries;

public static class GetCustomerByIdQuery
{
    public record Argument(int CustomerId) : IRequest<CustomerResult?>;
    
    public class Handler : IRequestHandler<Argument, CustomerResult?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        
        public Handler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResult?> Handle(Argument request, CancellationToken cancellationToken = default)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            
            return customer == null ? null : _mapper.Map<CustomerResult>(customer);
        }
    }
}