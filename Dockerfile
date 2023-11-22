# Use the .NET 6 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the remaining files and build the application
COPY . .
RUN dotnet publish -c Release -o out


# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Specify environment variables required by your application
ENV DATABASE_URL=${DATABASE_URL}
ENV MYSQL_HOST=${MYSQL_HOST}
ENV MYSQL_PORT=${MYSQL_PORT}
ENV MYSQL_USER=${MYSQL_USER}
ENV MYSQL_PASSWORD=${MYSQL_PASSWORD}
ENV MYSQL_DATABASE=${MYSQL_DATABASE}
ENV MYSQL_SSLMODE = ${MYSQL_SSLMODE}
ENV MYSQL_DEFAULTCOMMANDTIMEOUT = ${MYSQL_DEFAULTCOMMANDTIMEOUT}

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll", "--environment=Development"]
