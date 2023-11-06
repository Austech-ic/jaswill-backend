# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Install MySQL client
RUN apt-get update && apt-get install -y mariadb-client


# Expose the port your application is running on
EXPOSE 80

# Set environment variables for MySQL connection
ENV MYSQL_HOST=localhost
ENV MYSQL_PORT=3306
ENV MYSQL_DATABASE=JaswillRealEstate
ENV MYSQL_USER=root
ENV MYSQL_PASSWORD=/run/secrets/mysql_password_secret

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
