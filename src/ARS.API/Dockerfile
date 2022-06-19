FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ARS.API/ARS.API.csproj", "ARS.API/"]
RUN dotnet restore "ARS.API/ARS.API.csproj"
COPY . .
WORKDIR "/src/ARS.API"
RUN dotnet build "ARS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ARS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ARS.API.dll"]
