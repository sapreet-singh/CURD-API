# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Force IPv4 during restore/build
ENV DOTNET_SYSTEM_NET_SOCKETS_USEONLYIPV4=1
ENV NPGSQL_IPADDRESSFAMILY=InterNetwork

# Copy the project file and restore dependencies
COPY ["CURD-API/CURD-API.csproj", "CURD-API/"]
RUN dotnet restore "CURD-API/CURD-API.csproj"

# Copy the rest of the application code
COPY . .

# Build the application
WORKDIR "/src/CURD-API"
RUN dotnet build "CURD-API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "CURD-API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Force IPv4 inside final runtime container
ENV DOTNET_SYSTEM_NET_SOCKETS_USEONLYIPV4=1
ENV NPGSQL_IPADDRESSFAMILY=InterNetwork

# Copy the published application
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "CURD-API.dll"]
