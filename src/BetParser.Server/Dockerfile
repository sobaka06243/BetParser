#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BetParser.Server/BetParser.Server.csproj", "BetParser.Server/"]
COPY ["BetParser.Contracts/BetParser.Contracts.csproj", "BetParser.Contracts/"]
COPY ["BetParser.Data/BetParser.Data.csproj", "BetParser.Data/"]
RUN dotnet restore "BetParser.Server/BetParser.Server.csproj"
COPY . .
WORKDIR "/src/BetParser.Server"
RUN dotnet build "BetParser.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BetParser.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BetParser.Server.dll"]