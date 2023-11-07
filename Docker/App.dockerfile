# Use the official .NET SDK image as a base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the build configuration to Debug
ENV BUILD_CONFIG Debug

# Set maintainer information and labels
LABEL maintainer=some_email@email_server.com \
    Name=jaswillbackend-${BUILD_CONFIG} \
    Version=0.0.1

# Set the URL port from the build argument
ARG URL_PORT
ENV ASPNETCORE_URLS http://*:${URL_PORT}

# Set working directory
WORKDIR /app

# Skip NuGet XML documentation
ENV NUGET_XMLDOC_MODE skip

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

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
