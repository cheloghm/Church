FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5159

ENV ASPNETCORE_URLS=http://+:5159

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Church.csproj", "./"]
RUN dotnet restore "Church.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Church.csproj" -c $configuration -o /app/build

# Copy the CA certificate into the image
COPY rds-combined-ca-bundle.pem /etc/ssl/certs/
COPY eu-west-2-bundle.pem /etc/ssl/certs/

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Church.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

# Copy the CA certificate from the build stage
COPY --from=build /etc/ssl/certs/rds-combined-ca-bundle.pem /etc/ssl/certs/
COPY --from=build /etc/ssl/certs/eu-west-2-bundle.pem /etc/ssl/certs/

# Copy the application
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Church.dll"]
