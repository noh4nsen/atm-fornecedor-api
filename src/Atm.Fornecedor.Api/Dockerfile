#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ARG HTTP_PORT
ARG HTTPS_PORT
COPY ["certificates/*", "https/"]
WORKDIR /app
EXPOSE ${HTTP_PORT} ${HTTPS_PORT}

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIG
WORKDIR /src
COPY ["src/Atm.Fornecedor.Api/Atm.Fornecedor.Api.csproj", "src/Atm.Fornecedor.Api/"]
COPY ["src/Atm.Fornecedor.Dados/Atm.Fornecedor.Dados.csproj", "src/Atm.Fornecedor.Dados/"]
COPY ["src/Atm.Fornecedor/Atm.Fornecedor.csproj", "src/Atm.Fornecedor/"]
#COPY ["tests/Atm.Fornecedor.Tests/Mps.Fornecedor.Tests.csproj", "tests/Atm.Fornecedor.Tests/"]
RUN dotnet restore "src/Atm.Fornecedor.Api/Atm.Fornecedor.Api.csproj"
COPY . .
WORKDIR "/src/src/Atm.Fornecedor.Api"
RUN dotnet build "Atm.Fornecedor.Api.csproj" -c ${BUILD_CONFIG} -o /app/build

#FROM build AS tests
#WORKDIR /src/tests/Atm.Fornecedor.Tests
#RUN dotnet tool install dotnet-reportgenerator-globaltool --tool-path /dotnetglobaltools
#RUN dotnet add package coverlet.msbuild
#RUN dotnet restore "Atm.Fornecedor.Tests.csproj"
#RUN dotnet test /p:CollectCoverage=true /p:Threshold=95 /p:ThresholdType=line /p>ThresholdStat=total /p:CoverletOutputFormat=cobertura /p:Exclute="[*]Atm.Fornecedor.Dados.*" /p:CoverletOutput='./results/'
#RUN dotnet test --logger:"xunit;LogFilePath=./results/xunit-result.xml"
#RUN /dotnetglobaltools/reportgenerator "-reports:results/coverage.cobertura.xml" "-targetdir:./results/reports" "-reporttypes:HTMLInline;HTMLChart"

FROM build AS publish
ARG BUILD_CONFIG
RUN dotnet publish "Atm.Fornecedor.Api.csproj" -c {BUILD_CONFIG} -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#COPY --from=tests src/tests/Atm.Fornecedor.Tests/results ./results

ENTRYPOINT ["dotnet", "Atm.Fornecedor.Api.dll"]
