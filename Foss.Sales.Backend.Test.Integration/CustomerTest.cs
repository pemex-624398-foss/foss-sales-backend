using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Pemex.Foss.Sales.Backend.Test.Integration.Extensions;
using Xunit;

namespace Pemex.Foss.Sales.Backend.Test.Integration;

public class CustomerTest
{
    private record CreateCustomerCommandArgument(
        string FirstName,
        string? LastName,
        string? Email,
        string? Gender,
        string City
    );

    private record CreateCustomerCommandResult(int CustomerId);
    
    private record CustomerQueryResult(
        int Id,
        string FirstName,
        string? LastName,
        string? Email,
        string? Gender,
        string City
    );

    private record RenameCustomerCommandArgument(string FirstName, string LastName);
    private record UpdateCustomerEmailCommandArgument(string Email);
    
    [Fact]
    public async Task Creates_Gets_Renames_Updates_And_Removes_A_Customer()
    {
        const string baseRoute = "api/customers";
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        
        //
        // Create
        //
        var createCustomerCommandArgument = new CreateCustomerCommandArgument(
            "Maria",
            "Perez",
            "maria.perez@pemex.com", 
            "Female", 
            "México"
            );
        
        var createCustomerResponse = await httpClient.PostAsJsonAsync(baseRoute, createCustomerCommandArgument);
        var createCustomerCommandResult = await createCustomerResponse.Content.ReadAsAsync<CreateCustomerCommandResult>();
        
        createCustomerResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createCustomerCommandResult.Should().NotBeNull();
        createCustomerCommandResult?.CustomerId.Should().BeGreaterThan(0);
        
        
        //
        // New Customer Id
        var customerId = createCustomerCommandResult?.CustomerId;
        
        
        //
        // Get By Id
        //
        var getByIdResponse = await httpClient.GetAsync($"{baseRoute}/{customerId}");
        var customer = await getByIdResponse.Content.ReadAsAsync<CustomerQueryResult>();
        
        getByIdResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        customer.Should().NotBeNull();
        customer?.Id.Should().Be(customerId);
        
        
        //
        // Get By Filter
        //
        const string gender = "Female";
        var getByFilterResponse = await httpClient.GetAsync($"{baseRoute}?gender={gender}");
        var customers =
            (await getByFilterResponse.Content.ReadAsAsync<IEnumerable<CustomerQueryResult>>() 
             ?? Array.Empty<CustomerQueryResult>()).ToArray();
        
        getByFilterResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        customers.Should().NotBeEmpty();
        
        
        //
        // Rename
        //
        var renameCustomerCommandArgument = new RenameCustomerCommandArgument("Norma", "Juarez");
        var renameCustomerResponse =
            await httpClient.PutAsJsonAsync($"{baseRoute}/{customerId}", renameCustomerCommandArgument);
        
        renameCustomerResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        //
        // Update Email
        //
        var updateCustomerEmailCommandArgument = new UpdateCustomerEmailCommandArgument("norma.juarez@pemex.com");
        var updateCustomerEmailResponse =
            await httpClient.PutAsJsonAsync($"{baseRoute}/{customerId}/email", updateCustomerEmailCommandArgument);
      
        updateCustomerEmailResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        
        //
        // Delete
        //
        var deleteCustomerResponse = await httpClient.DeleteAsync($"{baseRoute}/{customerId}");
        
        deleteCustomerResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}