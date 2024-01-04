FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/TeamManager.Api/TeamManager.Api.csproj", "src/TeamManager.Api/"]
RUN dotnet restore "src/TeamManager.Api/TeamManager.Api.csproj"
COPY . .
WORKDIR "/src/src/TeamManager.Api"
RUN dotnet build "TeamManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TeamManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TeamManager.Api.dll"]