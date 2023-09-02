#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SureSellOrganizers_API/SureSellOrganizers_API.csproj", "SureSellOrganizers_API/"]
COPY ["ProductMove_Model/ProductMove_Model.csproj", "ProductMove_Model/"]
RUN dotnet restore "SureSellOrganizers_API/SureSellOrganizers_API.csproj"
COPY . .
WORKDIR "/src/SureSellOrganizers_API"
RUN dotnet build "SureSellOrganizers_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SureSellOrganizers_API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SureSellOrganizers_API.dll"]