namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public class MultipleEntityAffected : ModelException
{
    public MultipleEntityAffected(string message) : base(message)
    {
    }
}