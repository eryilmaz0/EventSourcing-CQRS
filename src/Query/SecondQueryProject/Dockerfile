﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Query/SecondQueryProject/SecondQueryProject.csproj", "Query/SecondQueryProject/"]
RUN dotnet restore "Query/SecondQueryProject/SecondQueryProject.csproj"
COPY . .
WORKDIR "/src/Query/SecondQueryProject"
RUN dotnet build "SecondQueryProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondQueryProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondQueryProject.dll"]
