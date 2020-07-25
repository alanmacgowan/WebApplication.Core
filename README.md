# WebApplication.Core
Sample Asp.Net Core Application for CI/CD

## Tools:
Area |Tools
-----|------
CI | Jenkins 
Source Code | GitHub
Issue Management | GitHub
Tech stack | ASP.Net Core MVC, EF Core, JQuery, Bootstrap, Webpack, AutoMapper
Database | SQL Server
Web Server | IIS
Unit & Integration Testing | MSTest, Moq
Acceptance Testing | Specflow, Selenium
API Testing | Postman, newman
Performance Testing | k6
Build Tools |MSBuild, MSDeploy
Test Coverage | Coverlet
Code Metrics | Sonarqube
Dependencies | Nuget, npm
Scripts | Powershell
Containers | Docker, Docker Compose, Kubernetes

# Setup

git clone https://github.com/alanmacgowan/WebApplication.Core.git

## Dotnet:
*dotnet-run.bat*: starts API and UI applications.
Assumes there is a database already in local SQL Server named [Database]
```
.\build\dotnet-run.bat
```
Navigate to http://localhost:5006/ for UI and http://localhost:5000/swagger/index for API.

## Docker
Can use images from Dockerhub:
```
docker network create webappnet
docker run -e "ASPNETCORE_ENVIRONMENT=Development" -e "WebApplicationAPIUrl=http://webapplicationcore-api" -p 8040:80 --name webapplicationcore-ui --network webappnet -d alanmacgowan/webapplication.core.ui
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pass@word" -p 5433:1433 --name sqldata --network webappnet -d mcr.microsoft.com/mssql/server:2017-latest
docker run -e "ASPNETCORE_ENVIRONMENT=Development" -e "ConnectionString=Server=sqldata;Database=Database;User Id=sa;Password=Pass@word" -p 8048:80 --name webapplicationcore-api --network webappnet -d alanmacgowan/webapplication.core.api
```
SQL Server container might take a couple of minutes to start, API has retry logic with Polly in order to run ef migrations.
Navigate to http://localhost:8040/ for UI and http://localhost:8048/swagger/index for API.

To stop:
```
docker stop webapplicationcore-ui
docker stop webapplicationcore-api
docker stop sqldata
```

## Docker Compose
*docker-compose-up.ps1*: Build images and start services for different environments.
```
./docker-compose-up.ps1 -environment "staging"
```
SQL Server container might take a couple of minutes to start, API has retry logic with Polly in order to run ef migrations.
Navigate to http://localhost:8040/ for UI and http://localhost:8048/swagger/index for API.

To stop:
```
docker-compose down
```

## Kubernetes
k8s folder contains the deployment and service config files for Kubernetes.

*kubernetes-deploy.ps1*: Build, tag and push (optional) images to Dockerhub and deploy to a local kubernetes cluster.
```
cd webapplication.core\k8s
./kubernetes-deploy.ps1 -dockerUser user -dockerPassword pass
```
Sql Server container might take a couple of minutes to start, API has retry logic with Polly in order to run ef migrations.
Navigate to http://localhost:30001/ 

To delete:
```
cd webapplication.core\k8s
kubectl delete -f ./
```

## Local IIS deployment
*iis-deploy.ps1*: Powershell script to build, deploy, test and package .net core web application to a local IIS.

From Powershell:
```
./iis-deploy.ps1 -Version "1.0.0.0" -Environment "Stage" -Branch "master" -CleanEnvironment $true
```
This assumes that a web site is already setup on local IIS.

## Continuous Integration

#### Jenkins
*jenkinsfile*
