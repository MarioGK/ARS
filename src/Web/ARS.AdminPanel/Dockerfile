FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Web/Store.AdminPanel/Store.AdminPanel.csproj", "Store.AdminPanel/"]
RUN dotnet restore "Web/Store.AdminPanel/Store.AdminPanel.csproj"
COPY . .
WORKDIR "/src/Store.AdminPanel"
RUN dotnet build "Store.AdminPanel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Store.AdminPanel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Store.AdminPanel.dll"]
