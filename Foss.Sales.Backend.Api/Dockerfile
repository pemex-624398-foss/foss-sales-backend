FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY bin/Release/net6.0/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Foss.Sales.Backend.Api.dll"]





#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj", "Foss.Sales.Backend.Api/"]
#RUN dotnet restore "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"
#COPY . .
#WORKDIR "/src/Foss.Sales.Backend.Api"
#RUN dotnet build "Foss.Sales.Backend.Api.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Foss.Sales.Backend.Api.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Foss.Sales.Backend.Api.dll"]
