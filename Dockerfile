# Use the official ASP.NET runtime image for the final app image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

# Use the SDK image for building and restoring dependencies
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy and restore the project files for each layer, ensuring all dependencies are restored
COPY ["ExpenseReportAPI/ExpenseReportAPI.csproj", "ExpenseReportAPI/"]
COPY ["Business Layer/Business Layer.csproj", "Business Layer/"]
COPY ["Data Access Layer/Data Access Layer.csproj", "Data Access Layer/"]
COPY ["Shared Layer/Shared Layer.csproj", "Shared Layer/"]

# Restore dependencies for the entire solution (all layers and projects)
RUN dotnet restore "Shared Layer/Shared Layer.csproj"
RUN dotnet restore "Data Access Layer/Data Access Layer.csproj"
RUN dotnet restore "Business Layer/Business Layer.csproj"
RUN dotnet restore "ExpenseReportAPI/ExpenseReportAPI.csproj"

# Copy all source code files
COPY . .

# Build the application with the specified configuration
WORKDIR "/src/ExpenseReportAPI"
RUN dotnet build "ExpenseReportAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the final output
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExpenseReportAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseReportAPI.dll"]
