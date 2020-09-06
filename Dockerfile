FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers

COPY Infraestructura/*.csproj ./Infraestructura/
COPY Dominio/*.csproj ./Dominio/
COPY Aplicacion/*.csproj ./Aplicacion/

COPY *.sln ./

# Restore dependencies

RUN dotnet restore

# Copy everything else and build

COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
ENV ASPNETCORE_URLS=http://+;https://+
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=build /app/out .

# Lauch server

ENTRYPOINT ["dotnet", "Infraestructura.dll"]