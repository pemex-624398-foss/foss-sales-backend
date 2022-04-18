using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Pemex.Foss.Sales.Backend.Api.Core.Model;
using Pemex.Foss.Sales.Backend.Api.Infrastructure.Sql.Repositories;
using Serilog;

const string logDirectory = "Logs";

try
{
    if (!Directory.Exists(logDirectory))
        Directory.CreateDirectory(logDirectory);
    
    var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.
    
    var configuration = builder.Configuration;
    
    //
    // Logging
    //
    builder.Host.UseSerilog((_, config) =>
    {
        config.ReadFrom.Configuration(configuration);
    });

    //
    // SQL Database Infrastructure
    //
    const string connectionStringKey = "Default";
    var connectionString = configuration.GetConnectionString(connectionStringKey);
    var connectionProviderName = configuration["ConnectionProvider"];

    builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
        new DbConnectionFactory(connectionStringKey, connectionProviderName, connectionString));

    builder.Services.AddTransient<ICustomerRepository>(provider =>
        new CustomerRepository(provider.GetRequiredService<IDbConnectionFactory>()));
    
    //
    // Mediator Pattern & Object Mapping
    //
    var assembly = Assembly.GetExecutingAssembly();
    
    builder.Services.AddAutoMapper(assembly);
    builder.Services.AddMediatR(assembly);

    //
    // Controllers
    //
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.CustomSchemaIds(type => type.IsNested ? $"{type.DeclaringType?.Name ?? ""}{type.Name}" : type.Name);
    });
    builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(assembly));

    //
    // Build & Configure Application
    //
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseSerilogRequestLogging();

    //
    // Run Application
    //
    app.Run();
}
catch (Exception exception)
{
    try
    {
        File.WriteAllText(
            Path.Combine(logDirectory, $"exception-{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt"),
            exception.ToString()
            );
    }
    catch
    {
        Console.WriteLine(exception.ToString());
    }
}
public partial class Program { }