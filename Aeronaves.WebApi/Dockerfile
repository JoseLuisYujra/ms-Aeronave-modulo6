#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Aeronaves.WebApi/Aeronaves.WebApi.csproj", "Aeronaves.WebApi/"]
COPY ["Shared.Rabbitmq/Shared.Rabbitmq.csproj", "Shared.Rabbitmq/"]
RUN dotnet restore "Aeronaves.WebApi/Aeronaves.WebApi.csproj"
COPY . .
WORKDIR "/src/Aeronaves.WebApi"
RUN dotnet build "Aeronaves.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aeronaves.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aeronaves.WebApi.dll"]