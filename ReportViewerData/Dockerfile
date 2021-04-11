#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ReportViewerData/ReportViewerData.csproj", "ReportViewerData/"]
RUN dotnet restore "ReportViewerData/ReportViewerData.csproj"
COPY . .
WORKDIR "/src/ReportViewerData"
RUN dotnet build "ReportViewerData.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportViewerData.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
CMD mkdir /app/pdf
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportViewerData.dll"]