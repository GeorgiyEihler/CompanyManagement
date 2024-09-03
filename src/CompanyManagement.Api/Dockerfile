#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5830
EXPOSE 5831

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/CompanyManagement.Api/CompanyManagement.Api.csproj", "src/CompanyManagement.Api/"]
COPY ["src/CompanyManagement.Application/CompanyManagement.Application.csproj", "src/CompanyManagement.Application/"]
COPY ["src/CompanyManagement.Domain/CompanyManagement.Domain.csproj", "src/CompanyManagement.Domain/"]
COPY ["src/CompanyManagement.Infrastructure/CompanyManagement.Infrastructure.csproj", "src/CompanyManagement.Infrastructure/"]
RUN dotnet restore "./src/CompanyManagement.Api/CompanyManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/CompanyManagement.Api"
RUN dotnet build "./CompanyManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CompanyManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompanyManagement.Api.dll"]