# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./s3-individueel/S3 Backend/S3 Backend.csproj" --disable-parallel
RUN dotnet publish "./s3-individueel/S3 Backend/S3 Backend.csproj" -c release -o /app --no-restore

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
EXPOSE 80

ENTRYPOINT ["dotnet", "S3 Backend.dll"]