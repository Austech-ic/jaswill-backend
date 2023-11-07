# Use a base image with .NET SDK to build your application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy necessary files
COPY . .

# Build the application
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Install PostgreSQL client
RUN apt-get update && apt-get install -y postgresql-client

# Set environment variables for PostgreSQL connection
ENV PGHOST=dpg-cl50f8s72pts739olh2g-a.oregon-postgres.render.com
ENV PGPORT=5432
ENV PGDATABASE=jaswillbackend
ENV PGUSER=jaswillbackend_user
ENV PGPASSWORD=1Pzo6Mbzt4SwYYaoaMc1lCpPphqwRU4n

# Expose the port your application is running on
EXPOSE 80

# Define the entry point for your application
ENTRYPOINT ["dotnet", "CMS_appBackend.dll"]
