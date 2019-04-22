FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk-alpine AS build
WORKDIR /src
COPY src/PocketGym.API/PocketGym.API.csproj ./PocketGym.API/
COPY src/PocketGym.Application/PocketGym.Application.csproj ./PocketGym.Application/
COPY src/PocketGym.Domain/PocketGym.Domain.csproj ./PocketGym.Domain/
COPY src/PocketGym.Domain.Core/PocketGym.Domain.Core.csproj ./PocketGym.Domain.Core/
COPY src/PocketGym.Application.Core/PocketGym.Application.Core.csproj ./PocketGym.Application.Core/
COPY src/PocketGym.Infrastructure.CrossCutting/PocketGym.Infrastructure.CrossCutting.csproj ./PocketGym.Infrastructure.CrossCutting/
COPY src/PocketGym.Infrastructure.Repository.LiteDb/PocketGym.Infrastructure.Repository.LiteDb.csproj ./PocketGym.Infrastructure.Repository.LiteDb/
RUN dotnet restore ./PocketGym.API/PocketGym.API.csproj
COPY . .
WORKDIR /src/src/PocketGym.API
RUN dotnet build PocketGym.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PocketGym.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet PocketGym.API.dll
