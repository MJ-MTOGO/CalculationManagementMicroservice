FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy only the main project files
COPY CalculationManagementService/*.csproj ./CalculationManagementService/
RUN dotnet restore CalculationManagementService/CalculationManagementService.csproj

# Copy the rest of the files
COPY . ./
RUN dotnet publish CalculationManagementService/CalculationManagementService.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .

ENTRYPOINT ["dotnet", "CalculationManagementService.dll"]











