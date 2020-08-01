#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApplication.Core.UI/WebApplication.Core.UI.csproj", "WebApplication.Core.UI/"]
COPY ["WebApplication.Core.Data/WebApplication.Core.Data.csproj", "WebApplication.Core.Data/"]
COPY ["WebApplication.Core.Domain/WebApplication.Core.Domain.csproj", "WebApplication.Core.Domain/"]
RUN dotnet restore "WebApplication.Core.UI/WebApplication.Core.UI.csproj"
COPY . .
WORKDIR "/src/WebApplication.Core.UI"
RUN dotnet build "WebApplication.Core.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApplication.Core.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication.Core.UI.dll"]
