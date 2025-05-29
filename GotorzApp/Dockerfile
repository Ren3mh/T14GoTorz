# Use Ubuntu as the base image for the build stage
FROM ubuntu:20.04 AS build

# Install dependencies for .NET
RUN apt-get update && \
    apt-get install -y wget apt-transport-https software-properties-common && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-sdk-9.0

# Set the working directory
WORKDIR /src

# Copy the project file and restore dependencies
COPY GotorzApp/GotorzApp.csproj ./GotorzApp/
RUN dotnet restore ./GotorzApp/GotorzApp.csproj

# Copy the rest of the source code and build the application
COPY . .
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use Ubuntu as the base image for the runtime stage
FROM ubuntu:20.04 AS final

# Install dependencies for .NET runtime
RUN apt-get update && \
    apt-get install -y wget apt-transport-https software-properties-common && \
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y aspnetcore-runtime-9.0

# Set the working directory
WORKDIR /app

# Copy the published application from the build stage
COPY --from=publish /app/publish .

# Expose port 80
EXPOSE 80

# Set the ASPNETCORE_ENVIRONMENT environment variable
ENV ASPNETCORE_ENVIRONMENT=Production

# Set the entry point
ENTRYPOINT ["dotnet", "GotorzApp.dll"]

ARG APPSETTINGS_JSON_BASE64
RUN echo "$APPSETTINGS_JSON_BASE64" | base64 -d > /app/appsettings.Production.json