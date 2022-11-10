# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./AnstigramAPI/AnstigramAPI.csproj" --disable-parallel
RUN dotnet publish "./AnstigramAPI/AnstigramAPI.csproj" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
EXPOSE 80

ENTRYPOINT ["dotnet", "AnstigramAPI.dll"]