#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/eCommerceBase/eCommerceBase/eCommerceBase.Api/eCommerceBase.Api.csproj", "Services/eCommerceBase/eCommerceBase/eCommerceBase.Api/"]
COPY ["Services/eCommerceBase/eCommerceBase/eCommerceBase.Application/eCommerceBase.Application.csproj", "Services/eCommerceBase/eCommerceBase/eCommerceBase.Application/"]
COPY ["Services/eCommerceBase/eCommerceBase/eCommerceBase.Domain/eCommerceBase.Domain.csproj", "Services/eCommerceBase/eCommerceBase/eCommerceBase.Domain/"]
COPY ["Services/eCommerceBase/eCommerceBase/eCommerceBase.Insfrastructure/eCommerceBase.Insfrastructure.csproj", "Services/eCommerceBase/eCommerceBase/eCommerceBase.Insfrastructure/"]
COPY ["Services/eCommerceBase/eCommerceBase/eCommerceBase.Persistence/eCommerceBase.Persistence.csproj", "Services/eCommerceBase/eCommerceBase/eCommerceBase.Persistence/"]
RUN dotnet restore "./Services/eCommerceBase/eCommerceBase/eCommerceBase.Api/./eCommerceBase.Api.csproj"
COPY . .
WORKDIR "/src/Services/eCommerceBase/eCommerceBase/eCommerceBase.Api"
RUN dotnet build "./eCommerceBase.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./eCommerceBase.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerceBase.Api.dll"]