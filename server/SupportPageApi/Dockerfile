FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "SupportPageApi.dll"]

# 先修改 appsettings.json 的DB連線字串
# sudo docker build -t support_api -f Dockerfile .
# sudo docker rm -f support_api
# sudo docker run -d --name support_api -p 50100:8080 support_api
