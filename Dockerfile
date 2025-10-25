# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY Backend/RecipeParser.Api/RecipeParser.Api.csproj ./Backend/RecipeParser.Api/
RUN dotnet restore Backend/RecipeParser.Api/RecipeParser.Api.csproj

# Copy source code and build
COPY Backend/RecipeParser.Api/ ./Backend/RecipeParser.Api/
RUN dotnet build Backend/RecipeParser.Api/RecipeParser.Api.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish Backend/RecipeParser.Api/RecipeParser.Api.csproj -c Release -o /app/publish

# Use the official .NET 8 runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published application
COPY --from=build /app/publish .

# Expose port 8080 (Render's default)
EXPOSE 8080

# Set the entry point
ENTRYPOINT ["dotnet", "RecipeParser.Api.dll"]