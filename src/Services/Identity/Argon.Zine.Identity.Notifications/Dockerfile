#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Identity/Argon.Zine.Identity.Notifications/Argon.Zine.Identity.Notifications.csproj", "src/Services/Identity/Argon.Zine.Identity.Notifications/"]
RUN dotnet restore "src/Services/Identity/Argon.Zine.Identity.Notifications/Argon.Zine.Identity.Notifications.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Argon.Zine.Identity.Notifications"
RUN dotnet build "Argon.Zine.Identity.Notifications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Argon.Zine.Identity.Notifications.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Argon.Zine.Identity.Notifications.dll"]