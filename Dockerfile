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

# Install PostgreSQL client
RUN apt-get update && apt-get install -y postgresql-client

# Expose the port your application is running on
EXPOSE 80

# Set environment variables for PostgreSQL connection
ENV DATABASE_URL=postgres://jaswillbackend_user:1Pzo6Mbzt4SwYYaoaMc1lCpPphqwRU4n@dpg-cl50f8s72pts739olh2g-a.oregon-postgres.render.com/jaswillbackend=myrenderdbname.render.com;Port=5432;Database=jaswillbackend;User Id=jaswillbackend_user;Password=1Pzo6Mbzt4SwYYaoaMc1lCpPphqwRU4n;

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
