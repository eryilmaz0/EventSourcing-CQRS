FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Command/Command.API/Command.API.csproj", "Command/Command.API/"]
COPY ["Shared/EventSourcing.Shared/EventSourcing.Shared.csproj", "Shared/EventSourcing.Shared/"]
COPY ["Command/Core/Command.Application/Command.Application.csproj", "Command/Core/Command.Application/"]
COPY ["Command/Core/Command.Domain/Command.Domain.csproj", "Command/Core/Command.Domain/"]
COPY ["Command/Infrastructure/Command.Infrastructure/Command.Infrastructure.csproj", "Command/Infrastructure/Command.Infrastructure/"]
COPY ["Command/Infrastructure/Command.Persistence/Command.Persistence.csproj", "Command/Infrastructure/Command.Persistence/"]
RUN dotnet restore "Command/Command.API/Command.API.csproj"
COPY . .
WORKDIR "/src/Command/Command.API"
RUN dotnet build "Command.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Command.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Command.API.dll"]
