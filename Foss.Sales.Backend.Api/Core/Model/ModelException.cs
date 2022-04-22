namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public class ModelException : Exception
{
    public ModelException(string message) : base(message)
    {
    }

    public ModelException(string message, Exception innerException) : base(message, innerException)
    {
    }
}