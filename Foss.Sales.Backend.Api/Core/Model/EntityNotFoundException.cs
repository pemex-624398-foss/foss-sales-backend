namespace Pemex.Foss.Sales.Backend.Api.Core.Model;

public class EntityNotFoundException : ModelException
{
    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string message, object id) : this(message)
    {
        Id = id;
    }
    
    public object? Id { get; }
}