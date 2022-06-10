FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Query/Query.API/Query.API.csproj", "Query.API/"]
RUN dotnet restore "Query/Query.API/Query.API.csproj"
COPY . .
WORKDIR "/src/Query.API"
RUN dotnet build "Query.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Query.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Query.API.dll"]
