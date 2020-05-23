FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/WebApplication.Memcached/WebApplication.Memcached.csproj", "src/WebApplication.Memcached/"]
RUN dotnet restore "src/WebApplication.Memcached/WebApplication.Memcached.csproj"
COPY . .
WORKDIR "/src/src/WebApplication.Memcached"
RUN dotnet build "WebApplication.Memcached.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication.Memcached.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication.Memcached.dll"]