#Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-env

WORKDIR /app

COPY *.csproj ./
run dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:5.0 
ARG BUILD_CONFIGURATION=QuestPackages
ENV ASPNETCORE_ENVIRONMENT=Production 
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 80  
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "QuestPackages.dll"]