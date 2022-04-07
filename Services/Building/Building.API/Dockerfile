FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY "CleanMyHouse.sln" "CleanMyHouse.sln"
COPY "CleanMyHouse/CleanMyHouse.csproj" "CleanMyHouse/CleanMyHouse.csproj"
COPY "Services/User/User.API/User.API.csproj" "Services/User/User.API/User.API.csproj" 
COPY "Services/User/User.Application/User.Application.csproj" "Services/User/User.Application/User.Application.csproj" 
COPY "Services/User/User.Domain/User.Domain.csproj" "Services/User/User.Domain/User.Domain.csproj" 
COPY "Services/User/User.Persistence/User.Persistence.csproj" "Services/User/User.Persistence/User.Persistence.csproj"
COPY "Services/Building/Building.API/Building.API.csproj" "Services/Building/Building.API/Building.API.csproj"
COPY "Services/Building/Building.Integration/Building.Integration.csproj" "Services/Building/Building.Integration/Building.Integration.csproj"
COPY "EventBus/EventBus.csproj" "EventBus/EventBus.csproj"
COPY "EventBusRabbitMQ/EventBusRabbitMQ.csproj" "EventBusRabbitMQ/EventBusRabbitMQ.csproj"
COPY "EventBus/EventBus.csproj" "EventBus/EventBus.csproj"

COPY "docker-compose.dcproj" "docker-compose.dcproj"

RUN dotnet restore "CleanMyHouse.sln"

COPY . .
WORKDIR /src/Services/Building/Building.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Building.API.dll"]
