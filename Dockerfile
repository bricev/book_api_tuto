# Use the official .NET 9 SDK image as the build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file(s) and restore dependencies
# (Assuming your project file is named "Book_API.csproj")
COPY ["Book_API.csproj", "./"]
RUN dotnet restore "./Book_API.csproj"

# Copy the rest of the source code and build the project
COPY . .
RUN dotnet publish "./Book_API.csproj" -c Release -o /app/publish

# Use the ASP.NET Core 9 runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Set the app to listen only on HTTP
ENV ASPNETCORE_URLS="http://+:80"
EXPOSE 80

# Define the entry point for the container
ENTRYPOINT ["dotnet", "Book_API.dll"]
