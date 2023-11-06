# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1

# Set the working directory to /app
WORKDIR /app

# Copy the published output of your ASP.NET Core application into the container
COPY ./out/ .

# Expose the port that your application will run on
EXPOSE 80
# Define the command to run your application
CMD ["./CMS_appBackend"]
