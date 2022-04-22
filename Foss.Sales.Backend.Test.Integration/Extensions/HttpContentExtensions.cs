using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pemex.Foss.Sales.Backend.Test.Integration.Extensions;

public static class HttpContentExtensions
{
    public static async Task<T?> ReadAsAsync<T>(this HttpContent httpContent) where T : class 
        => JsonSerializer.Deserialize<T>(
            await httpContent.ReadAsStringAsync(),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
}