FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YarpApiGetway/YarpApiGetway/YarpApiGetway.csproj", "YarpApiGetway/YarpApiGetway/"]
RUN dotnet restore "YarpApiGetway/YarpApiGetway/YarpApiGetway.csproj"
COPY . .
WORKDIR "/src/YarpApiGetway/YarpApiGetway"
RUN dotnet build "YarpApiGetway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YarpApiGetway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YarpApiGetway.dll"]
