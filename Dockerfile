# Use the official ASP.NET image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Set the working directory in the container
WORKDIR /app

# Copy the published output of your MVC application to the container
COPY ./bin/Debug/net6.0 .

# Expose port 80 for HTTP traffic
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]