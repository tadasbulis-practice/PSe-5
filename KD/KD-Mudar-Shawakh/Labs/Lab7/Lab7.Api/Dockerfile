# Build stage — uses the full SDK to compile.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# Runtime stage — uses the slimmer aspnet image (no SDK).
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 6001
ENTRYPOINT ["dotnet", "Lab7.Api.dll"]