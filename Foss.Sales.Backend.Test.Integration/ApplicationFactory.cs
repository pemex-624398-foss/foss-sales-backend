using Microsoft.AspNetCore.Mvc.Testing;

namespace Pemex.Foss.Sales.Backend.Test.Integration;

public static class ApplicationFactory
{
    private static WebApplicationFactory<Program>? _applicationFactory;

    public static WebApplicationFactory<Program> Instance => 
        _applicationFactory ??= new WebApplicationFactory<Program>();
}