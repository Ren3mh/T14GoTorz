FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopier projektet og gendan afhængigheder
COPY *.csproj ./GotorzApp/
RUN dotnet restore

# Kopier resten af filerne og byg projektet
COPY . .
RUN dotnet build -c Release -o /app/build

# Publicer projektet
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Opbyg runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GotorzApp.dll"]
