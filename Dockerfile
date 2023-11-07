# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Install the dotenv tool
RUN dotnet tool install -g dotnet-dotenv

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./

# Load environment variables from the .env file
RUN dotnet dotenv add -p .

# Run dotnet publish
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Install PostgreSQL client
RUN apt-get update && apt-get install -y postgresql-client

# Expose the port your application is running on
EXPOSE 80

# Set environment variables for PostgreSQL connection
ENV Host=$Host
ENV Port=$Port
ENV Database=$Database
ENV Username=$Username
ENV Password=$Password

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
