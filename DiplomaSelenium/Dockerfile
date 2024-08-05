# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the project files
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build the project
RUN dotnet build --configuration Release --output /app/build

# Run the tests
CMD ["dotnet", "test", "--no-build", "--verbosity", "normal", "/app/build/DiplomaSelenium.dll"]