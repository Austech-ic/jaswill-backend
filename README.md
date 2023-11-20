# jaswill-backend

The following are the guidelines for the backend of the JASWill project.
install the following packages for .Net 6 SDK:
To install .NET 6, you can follow these general steps based on your operating system. Note that these instructions may change, and it's always a good idea to check the official .NET website for the latest guidance.
Windows: Visit the official .NET website: Download .NET

Select the ".NET 6.0 SDK" tab.

Choose the appropriate installer for your system (x64 or x86) and download it.

Run the installer and follow the instructions.

To verify the installation, open a command prompt or PowerShell window and run:

bash Copy code dotnet --version macOS: Visit the official .NET website: Download .NET

Select the ".NET 6.0 SDK" tab.

Download the macOS installer.

Open the downloaded package and follow the installation instructions.

To verify the installation, open a terminal and run:

bash Copy code dotnet --version Linux (Ubuntu): For Ubuntu, you can use the package manager.

Open a terminal.

Run the following commands:

bash Copy code sudo apt update sudo apt install -y apt-transport-https sudo apt install -y dotnet-sdk-6.0 To verify the installation, run:

bash Copy code dotnet --version Linux (Fedora): For Fedora, you can use the package manager.

Open a terminal.

Run the following commands:

bash Copy code sudo dnf install dotnet-sdk-6.0 To verify the installation, run:

bash Copy code dotnet --version

Create a new file called "appsettings.json" and add the following code:
{
"ConnectionStrings": { "ApplicationContext": "Server=localhost;Database=jaswill;User Id=postgres;Password=postgres;" }, "Mailgun": { "api-key": "the api key", "api-url": "the api url", }, "Logging": { "LogLevel": { "Default": "Information", "Microsoft.AspNetCore": "Warning" } }, "AllowedHosts": "*"

}

dotnet add package Microsoft.EntityFrameworkCore
run "dotnet restore" to restore all the packages
run "dotnet ef migrations add Jaswillabckend" to create the initial migration
run "dotnet ef database update" to update the database
run "dotnet build" to build the project
run "dotnet run" to run the project