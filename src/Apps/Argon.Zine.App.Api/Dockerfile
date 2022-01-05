#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Apps/Argon.Zine.App.Api/Argon.Zine.App.Api.csproj", "src/Apps/Argon.Zine.App.Api/"]
COPY ["src/Services/Identity/Argon.Zine.Identity/Argon.Zine.Identity.csproj", "src/Services/Identity/Argon.Zine.Identity/"]
COPY ["src/BuildingBlocks/Argon.Zine.Commom/Argon.Zine.Commom.csproj", "src/BuildingBlocks/Argon.Zine.Commom/"]
COPY ["src/Services/Restaurants/Argon.Zine.Restaurants.Application/Argon.Zine.Restaurants.Application.csproj", "src/Services/Restaurants/Argon.Zine.Restaurants.Application/"]
COPY ["src/Services/Restaurants/Argon.Zine.Restaurants.Domain/Argon.Zine.Restaurants.Domain.csproj", "src/Services/Restaurants/Argon.Zine.Restaurants.Domain/"]
COPY ["src/Services/Chat/Argon.Zine.Chat/Argon.Zine.Chat.csproj", "src/Services/Chat/Argon.Zine.Chat/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.QueryStack/Argon.Zine.Catalog.QueryStack.csproj", "src/Services/Catalog/Argon.Zine.Catalog.QueryStack/"]
COPY ["src/BuildingBlocks/Argon.Zine.Shared/Argon.Zine.Shared.csproj", "src/BuildingBlocks/Argon.Zine.Shared/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.Communication/Argon.Zine.Catalog.Communication.csproj", "src/Services/Catalog/Argon.Zine.Catalog.Communication/"]
COPY ["src/Services/Ordering/Argon.Zine.Ordering.Application/Argon.Zine.Ordering.Application.csproj", "src/Services/Ordering/Argon.Zine.Ordering.Application/"]
COPY ["src/Services/Ordering/Argon.Zine.Ordering.Domain/Argon.Zine.Ordering.Domain.csproj", "src/Services/Ordering/Argon.Zine.Ordering.Domain/"]
COPY ["src/Services/Restaurants/Argon.Zine.Restaurants.Infra.Data/Argon.Zine.Restaurants.Infra.Data.csproj", "src/Services/Restaurants/Argon.Zine.Restaurants.Infra.Data/"]
COPY ["src/Services/Restaurants/Argon.Zine.Restaurants.QueryStack/Argon.Zine.Restaurants.QueryStack.csproj", "src/Services/Restaurants/Argon.Zine.Restaurants.QueryStack/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.Infra.Data/Argon.Zine.Catalog.Infra.Data.csproj", "src/Services/Catalog/Argon.Zine.Catalog.Infra.Data/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.Domain/Argon.Zine.Catalog.Domain.csproj", "src/Services/Catalog/Argon.Zine.Catalog.Domain/"]
COPY ["src/Services/Customers/Argon.Zine.Customers.Infra.Data/Argon.Zine.Customers.Infra.Data.csproj", "src/Services/Customers/Argon.Zine.Customers.Infra.Data/"]
COPY ["src/Services/Customers/Argon.Zine.Customers.Application/Argon.Zine.Customers.Application.csproj", "src/Services/Customers/Argon.Zine.Customers.Application/"]
COPY ["src/Services/Customers/Argon.Zine.Customers.Domain/Argon.Zine.Customers.Domain.csproj", "src/Services/Customers/Argon.Zine.Customers.Domain/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.Infra.Caching/Argon.Zine.Catalog.Infra.Caching.csproj", "src/Services/Catalog/Argon.Zine.Catalog.Infra.Caching/"]
COPY ["src/BuildingBlocks/Argon.Zine.Storage/Argon.Zine.Storage.csproj", "src/BuildingBlocks/Argon.Zine.Storage/"]
COPY ["src/BuildingBlocks/Argon.Zine.EventSourcing/Argon.Zine.EventSourcing.csproj", "src/BuildingBlocks/Argon.Zine.EventSourcing/"]
COPY ["src/Services/Catalog/Argon.Zine.Catalog.Application/Argon.Zine.Catalog.Application.csproj", "src/Services/Catalog/Argon.Zine.Catalog.Application/"]
COPY ["src/Services/Basket/Argon.Zine.Basket/Argon.Zine.Basket.csproj", "src/Services/Basket/Argon.Zine.Basket/"]
COPY ["src/Services/Ordering/Argon.Zine.Ordering.Infra.Data/Argon.Zine.Ordering.Infra.Data.csproj", "src/Services/Ordering/Argon.Zine.Ordering.Infra.Data/"]
RUN dotnet restore "src/Apps/Argon.Zine.App.Api/Argon.Zine.App.Api.csproj"
COPY . .
WORKDIR "/src/src/Apps/Argon.Zine.App.Api"
RUN dotnet build "Argon.Zine.App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Argon.Zine.App.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Argon.Zine.App.Api.dll"]