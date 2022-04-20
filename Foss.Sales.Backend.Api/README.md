# FOSS Sales Backend API

## Deployment
```shell
cd /path/to/Foss.Sales.Backend.Api

# Build application in Debug configuration
dotnet restore
dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"

# Run tests
export ASPNETCORE_ENVIRONMENT=Development && dotnet test

# Build application in Release configuration
dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release

# Publish application
dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release

# Build Docker Image
docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest Foss.Sales.Backend.Api

# Push Docker Image
cat github-pat.txt | docker login ghcr.io -u adrian8167e --password-stdin
docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest

# Watch Docker Image
export GH_PAT=$(cat github-pat.txt) && docker run -d \
  --name foss-watchtower \
  -e REPO_USER=adrian8167e \
  -e REPO_PASS=$GH_PAT \
  -v /var/run/docker.sock:/var/run/docker.sock \
  containrrr/watchtower foss-sales-backend --debug
```