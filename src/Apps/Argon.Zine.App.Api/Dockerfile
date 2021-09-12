#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/WebApis/Argon.Zine.App.Api/Argon.Zine.App.Api.csproj", "src/WebApis/Argon.Zine.App.Api/"]
RUN dotnet restore "src/WebApis/Argon.Zine.App.Api/Argon.Zine.App.Api.csproj"
COPY . .
WORKDIR "/src/src/WebApis/Argon.Zine.App.Api"
RUN dotnet build "Argon.Zine.App.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Argon.Zine.App.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Argon.Zine.App.Api.dll"]