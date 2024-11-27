# Step 1: Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy the project file and restore dependencies
COPY CalculationManagementService.csproj ./CalculationManagementService.csproj
RUN dotnet restore ./CalculationManagementService.csproj

# Copy the rest of the application files and build the app
COPY . ./
RUN dotnet publish ./CalculationManagementService.csproj -c Release -o out

# Step 2: Use the ASP.NET Core runtime to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "CalculationManagementService.dll"]












