# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./AnstigramAPI/AnstigramAPI.csproj" --disable-parallel
RUN dotnet publish "./AnstigramAPI/AnstigramAPI.csproj" -c release -o /app --no-restore

WORKDIR /cert
RUN dotnet dev-certs https -ep certhttps.pxf -p Code123

# serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
COPY --from=build /app ./

WORKDIR /app
COPY --from=build /cert ./app

EXPOSE 5000
EXPOSE 80

ENTRYPOINT ["dotnet", "AnstigramAPI.dll"]