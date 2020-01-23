#FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
#WORKDIR /app
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
#WORKDIR /src
#COPY ["Svc.Implementation/Svc.Implementation.csproj", "Svc.Implementation/"]
#RUN dotnet restore "Svc.Implementation/Svc.Implementation.csproj"
#COPY . .
#WORKDIR "/src/Svc.Implementation"
#RUN dotnet build "Svc.Implementation.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "Svc.Implementation.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .





FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Svc.Implementation.csproj .
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 15008

ENTRYPOINT ["dotnet", "Svc.Implementation.dll"]