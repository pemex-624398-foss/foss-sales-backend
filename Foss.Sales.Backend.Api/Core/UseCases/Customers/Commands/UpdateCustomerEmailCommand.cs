using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;

namespace Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

public static class UpdateCustomerEmailCommand
{
    public class Argument : IRequest
    {
        public int CustomerId { get; set; }
        public string Email { get; set; } = "";
    }

    public class Handler : IRequestHandler<Argument>
    {
        private readonly ICustomerRepository _customerRepository;
        
        public Handler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(Argument request, CancellationToken cancellationToken)
        {
            await _customerRepository.UpdateEmailAsync(request.CustomerId, request.Email);
            return Unit.Value;
        }
    }
}