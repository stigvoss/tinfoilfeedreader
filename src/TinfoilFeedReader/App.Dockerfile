FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["TinfoilFeedReader.Server/TinfoilFeedReader.Server.csproj", "TinfoilFeedReader.Server/"]
COPY ["Module.Feeds.Infrastructure.EntityFrameworkCore/Module.Feeds.Infrastructure.EntityFrameworkCore.csproj", "Module.Feeds.Infrastructure.EntityFrameworkCore/"]
COPY ["Module.Feeds/Module.Feeds.csproj", "Module.Feeds/"]
COPY ["TinfoilFeedReader.Client/TinfoilFeedReader.Client.csproj", "TinfoilFeedReader.Client/"]
RUN dotnet restore "TinfoilFeedReader.Server/TinfoilFeedReader.Server.csproj"
COPY . .
WORKDIR "/src/TinfoilFeedReader.Server"
RUN dotnet build "TinfoilFeedReader.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TinfoilFeedReader.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TinfoilFeedReader.Server.dll"]