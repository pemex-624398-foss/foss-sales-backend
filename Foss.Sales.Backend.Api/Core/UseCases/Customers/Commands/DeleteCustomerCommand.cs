using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

public static class DeleteCustomerCommand
{
    public record Argument(int CustomerId) : IRequest;
    
    public class Handler : IRequestHandler<Argument>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public Handler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(Argument request, CancellationToken cancellationToken)
        {
            await _customerRepository.DeleteByIdAsync(request.CustomerId);
            return Unit.Value;
        }
    }
}