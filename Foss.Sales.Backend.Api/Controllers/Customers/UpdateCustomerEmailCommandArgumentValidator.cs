using FluentValidation;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

namespace Pemex.Foss.Sales.Backend.Api.Controllers.Customers;

public class UpdateCustomerEmailCommandArgumentValidator : AbstractValidator<UpdateCustomerEmailCommand.Argument>
{
    public UpdateCustomerEmailCommandArgumentValidator()
    {
        RuleFor(argument => argument.Email).NotNull().NotEmpty().MaximumLength(50);
    }
}