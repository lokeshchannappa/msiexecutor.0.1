FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app
COPY . .
RUN dotnet restore 
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .
#COPY Offline.Data.Uploader.msi .
ENTRYPOINT ["dotnet", "msiexecutor.0.1.dll"]