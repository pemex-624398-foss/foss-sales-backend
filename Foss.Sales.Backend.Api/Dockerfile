FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY bin/Release/net6.0/publish .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Pemex.Foss.Sales.Backend.Api.dll"]
