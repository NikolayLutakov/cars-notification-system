FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine as base
WORKDIR /app

EXPOSE 80

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:80

RUN apk update && apk add --no-cache icu-libs

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . ./
WORKDIR /app/CarsAPI

RUN dotnet build "CarsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsAPI.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "CarsAPI.dll" ]