FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TinfoilFeedReader.Agent/TinfoilFeedReader.Agent.csproj", "TinfoilFeedReader.Agent/"]
COPY ["Module.Feeds.Infrastructure.EntityFrameworkCore/Module.Feeds.Infrastructure.EntityFrameworkCore.csproj", "Module.Feeds.Infrastructure.EntityFrameworkCore/"]
COPY ["Module.Feeds/Module.Feeds.csproj", "Module.Feeds/"]
RUN dotnet restore "TinfoilFeedReader.Agent/TinfoilFeedReader.Agent.csproj"
COPY . .
WORKDIR "/src/TinfoilFeedReader.Agent"
RUN dotnet build "TinfoilFeedReader.Agent.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TinfoilFeedReader.Agent.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TinfoilFeedReader.Agent.dll"]