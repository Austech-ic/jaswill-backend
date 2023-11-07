# Use the official .NET SDK image as a base image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory in the container
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the application
COPY . ./
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory in the container
WORKDIR /app

# Copy the published application from the build image
COPY --from=build /app/out .

# Expose the port your application is running on
EXPOSE 80

# Set environment variables for PostgreSQL connection provided by Render
ENV Render_PostgreSQL_Host=$Render_PostgreSQL_Host
ENV Render_PostgreSQL_Port=$Render_PostgreSQL_Port
ENV Render_PostgreSQL_Database=$Render_PostgreSQL_Database
ENV Render_PostgreSQL_Username=$Render_PostgreSQL_Username
ENV Render_PostgreSQL_Password=$Render_PostgreSQL_Password

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
