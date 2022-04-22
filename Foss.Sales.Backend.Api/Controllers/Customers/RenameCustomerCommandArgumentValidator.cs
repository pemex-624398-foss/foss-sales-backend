using FluentValidation;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

namespace Pemex.Foss.Sales.Backend.Api.Controllers.Customers;

public class RenameCustomerCommandArgumentValidator : AbstractValidator<RenameCustomerCommand.Argument>
{
    public RenameCustomerCommandArgumentValidator()
    {
        RuleFor(argument => argument.FirstName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(argument => argument.LastName).MaximumLength(50);
    }
}