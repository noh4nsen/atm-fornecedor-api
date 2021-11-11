#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Atm.Fornecedor.Api/Atm.Fornecedor.Api.csproj", "src/Atm.Fornecedor.Api/"]
RUN dotnet restore "src/Atm.Fornecedor.Api/Atm.Fornecedor.Api.csproj"
COPY . .
WORKDIR "/src/src/Atm.Fornecedor.Api"
RUN dotnet build "Atm.Fornecedor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Atm.Fornecedor.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atm.Fornecedor.Api.dll"]
