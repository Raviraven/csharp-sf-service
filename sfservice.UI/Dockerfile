#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["sfservice.UI/sfservice.UI.csproj", "sfservice.UI/"]
RUN dotnet restore "sfservice.UI/sfservice.UI.csproj"
COPY . .
WORKDIR "/src/sfservice.UI"
RUN dotnet build "sfservice.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sfservice.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sfservice.UI.dll"]