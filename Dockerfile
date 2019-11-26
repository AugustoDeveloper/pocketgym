FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URL http://*:80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
COPY . .
WORKDIR /src/PocketGym.API
RUN dotnet publish PocketGym.API.csproj -c Release -o /app -r linux-x64

FROM base AS final
WORKDIR /app
COPY --from=build /app .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet PocketGym.API.dll
#CMD ["./PocketGym.API"]
