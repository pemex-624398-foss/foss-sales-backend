namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public class MultipleEntityAffectedException : ModelException
{
    public MultipleEntityAffectedException(string message) : base(message)
    {
    }
}