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
API Testing | Postman newman
Performance Testing | k6
Build Tools |MSBuild, MSDeploy
Test Coverage | Coverlet
Code Metrics | Sonarqube
Dependencies | Nuget, npm
Scripts | Powershell
Other | docker, docker-compose, Kubernetes

# Setup:

## Docker Compose:
*docker-compose-up.ps1*: Build images and start services for different environments.
```
./docker-compose-up.ps1 -environment "staging"
```
Sql Server container might take a couple of minutes to start, API has retry logic with Polly in order to run ef migrations.
Navigate to http://localhost:8040/ for UI and http://localhost:8048/swagger/index for API.

To stop:
```
docker-compose down
```

## Kubernetes:
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

## Local IIS deployment:
*iis-deploy.ps1*: Powershell script to build, deploy, test and package .net core web application to a local IIS.

From Powershell:
```
./iis-deploy -Version "1.0.0.0" -Environment "Stage" -Branch "master" -CleanEnvironment $true
```
This assumes that a web site is already setup on local IIS.

## Continuous Integration:

#### Jenkins:
*jenkinsfile*
