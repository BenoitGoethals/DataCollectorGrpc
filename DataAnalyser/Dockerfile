#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DataAnalyser/DataAnalyser.csproj", "DataAnalyser/"]
COPY ["Datacollector.core/Datacollector.core.csproj", "Datacollector.core/"]
COPY ["DataCollector.DataLayer/DataCollector.DataLayer.csproj", "DataCollector.DataLayer/"]
RUN dotnet restore "DataAnalyser/DataAnalyser.csproj"
COPY . .
WORKDIR "/src/DataAnalyser"
RUN dotnet build "DataAnalyser.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataAnalyser.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAnalyser.dll"]