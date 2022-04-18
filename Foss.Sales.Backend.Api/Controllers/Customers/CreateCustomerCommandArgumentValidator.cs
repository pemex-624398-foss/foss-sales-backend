using FluentValidation;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers;
using Pemex.Foss.Sales.Backend.Api.Core.UseCases.Customers.Commands;

namespace Pemex.Foss.Sales.Backend.Api.Controllers.Customers;

public class CreateCustomerCommandArgumentValidator : AbstractValidator<CreateCustomerCommand.Argument>
{
    public CreateCustomerCommandArgumentValidator()
    {
        RuleFor(argument => argument.FirstName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(argument => argument.LastName).MaximumLength(50);
        RuleFor(argument => argument.Gender).MaximumLength(50);
        RuleFor(argument => argument.Email).MaximumLength(50);
        RuleFor(argument => argument.City).NotNull().NotEmpty().MaximumLength(50);
    }
}