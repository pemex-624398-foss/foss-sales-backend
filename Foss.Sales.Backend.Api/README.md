FOSS Sales Backend API

```shell
cd /path/to/Foss.Sales.Backend.Api

# Build
dotnet restore
dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"

# Test
export ASPNETCORE_ENVIRONMENT=Staging && dotnet test

# Publish 
dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release
dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release

# Build and Push Docker Image
docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest Foss.Sales.Backend.Api

docker login ghcr.io -u adrian8167e
docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest
```