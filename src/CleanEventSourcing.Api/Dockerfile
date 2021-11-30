FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["CleanEventSourcing.Api/CleanEventSourcing.Api.csproj", "CleanEventSourcing.Api/"]
RUN dotnet restore "CleanEventSourcing.Api/CleanEventSourcing.Api.csproj"
COPY . .
WORKDIR "/src/CleanEventSourcing.Api"
RUN dotnet build "CleanEventSourcing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanEventSourcing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanEventSourcing.Api.dll"]
