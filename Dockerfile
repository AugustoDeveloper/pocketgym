FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0.1 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URL http://*:80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
COPY . .
WORKDIR /src/PocketGym.API
RUN dotnet publish PocketGym.API.csproj -c Release -o /app -r alpine-x64

FROM base AS final
WORKDIR /app
COPY --from=build /app .
RUN chmod 777 .
CMD ASPNETCORE_URLS=http://*:$PORT PocketGym.API
#CMD ["./PocketGym.API"]
