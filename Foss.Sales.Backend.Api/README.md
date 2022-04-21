# FOSS Sales Backend API

## Deployment
```shell
cd /path/to/Foss.Sales.Backend.Api

# Build application in Debug configuration
dotnet restore && dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"

# Run tests
export ASPNETCORE_ENVIRONMENT=Development && dotnet test

# Build and Publish Application in Release Configuration
dotnet clean "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release && \
  rm -rf Foss.Sales.Backend.Api/bin/Release/net6.0/publish && \
  dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release && \ 
  dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release

# Build Docker Image with Container Registry Tag
docker build -t adrian8167e/foss-sales-backend:624398-latest Foss.Sales.Backend.Api

# Push Docker Image and Run Container
docker push adrian8167e/foss-sales-backend:624398-latest
  
docker run -d --restart unless-stopped --network foss --name foss-sales-api -p 5555:5000 adrian8167e/foss-sales-backend:624398-latest

# Test the Container
curl "http://localhost:5555/api/customers?city=san" | jq
curl "http://localhost:5555/api/customers?gender=female"
curl "http://localhost:5555/api/customers?gender=female" | jq
curl "http://localhost:5555/api/customers?gender=female&email=.com" | jq
curl "http://localhost:5555/api/customers?gender=male&email=.com" | jq
curl "http://localhost:5555/api/customers?gender=female&email=.net" | jq
curl "http://localhost:5555/api/customers?email=.net" | jq length
curl "http://localhost:5555/api/customers?email=.com" | jq length
curl "http://localhost:5555/api/customers?email=.com" | jq '.[9:12]'
curl "http://localhost:5555/api/customers?email=.com" | jq '.[-4:]'
curl "http://localhost:5555/api/customers?email=.com" | jq '.[] | .firstName'
curl "http://localhost:5555/api/customers?email=.com" | jq '.[] | .email'
curl "http://localhost:5555/api/customers?email=.net" | jq '.[] | .city'


# Watch Docker Image with Container Watchtower
docker run --rm containrrr/watchtower -h

# Field name   | Mandatory? | Allowed values  | Allowed special characters
# ----------   | ---------- | --------------  | --------------------------
# Seconds      | Yes        | 0-59            | * / , -
# Minutes      | Yes        | 0-59            | * / , -
# Hours        | Yes        | 0-23            | * / , -
# Day of month | Yes        | 1-31            | * / , - ?
# Month        | Yes        | 1-12 or JAN-DEC | * / , -
# Day of week  | Yes        | 0-6 or SUN-SAT  | * / , - ?

docker run -d \
  --name foss-watchtower \
  --restart unless-stopped \
  -e TZ=America/Mazatlan \
  -e WATCHTOWER_SCHEDULE="* */2 * * * *" \
  -v /var/run/docker.sock:/var/run/docker.sock \
  containrrr/watchtower foss-sales-api --debug
```