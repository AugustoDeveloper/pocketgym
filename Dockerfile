FROM mcr.microsoft.com/dotnet/core/runtime-deps:2.2.6-alpine3.9 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URL http://*:80

FROM microsoft/dotnet:2.2-sdk AS build
COPY . .
WORKDIR /src/PocketGym.API

RUN dotnet restore PocketGym.API.csproj

FROM build AS publish
RUN dotnet publish PocketGym.API.csproj -c Release -o /app -r alpine-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
#CMD ASPNETCORE_URLS=http://*:80 dotnet PocketGym.API.dll
ENTRYPOINT ["./PocketGym.API"]
