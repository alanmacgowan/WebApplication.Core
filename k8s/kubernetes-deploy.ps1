<#
.Synopsis
Script to build, push and deploy docker containers to kubernetes cluster.

.DESCRIPTION
Script to build, push and deploy docker containers to kubernetes cluster.

.EXAMPLE
./kubernetes-deploy.ps1 -dockerUser User -dockerPassword SecretPassword 

#>

#Script kubernetes-deploy
#Creator Alan Macgowan
#Date 07/23/2020
#Updated
#References

Param(
    [parameter(Mandatory=$false)][string]$dockerUser,
    [parameter(Mandatory=$false)][string]$dockerPassword,
    [parameter(Mandatory=$false)][string]$imageTag="latest",
    [parameter(Mandatory=$false)][bool]$buildImages=$false,
    [parameter(Mandatory=$false)][bool]$pushImages=$false,
    [parameter(Mandatory=$false)][string]$dockerOrg="alanmacgowan"
)

# check required commands 
function Check-Commands{
    $requiredCommands = ("docker", "docker-compose", "kubectl")
    foreach ($command in $requiredCommands) {
        if ((Get-Command $command -ErrorAction SilentlyContinue) -eq $null) {
            Write-Host "$command must be on path" -ForegroundColor Red
            exit
        }
    }
}

# login to dockerhub
function Docker-Login{
    if (-not [string]::IsNullOrEmpty($dockerUser)) {
        Write-Host "Logging in to dockerhub as user $dockerUser" -ForegroundColor Yellow
        docker login -u $dockerUser -p $dockerPassword
    
        if (-not $LastExitCode -eq 0) {
            Write-Host "Login failed" -ForegroundColor Red
            exit
        }
    }
}

# building  docker images if needed
function Build-Images{
    if ($buildImages) {
        Write-Host "Building Docker images tagged with '$imageTag'" -ForegroundColor Yellow
        $env:TAG=$imageTag
        docker-compose -p .. -f ../docker-compose.yml -f ../docker-compose.production.yml build    
    }
}

# push images to dockerhub
function Push-Images{
    if ($pushImages) {
        Write-Host "Pushing images to $registry/$dockerOrg..." -ForegroundColor Yellow
        $services = ("webapplication.core.ui", "webapplication.core.api")
        foreach ($service in $services) {
            $imageFqdn = "$dockerOrg/${service}"
            docker tag ${service}:$imageTag ${imageFqdn}:$imageTag
            docker push ${imageFqdn}:$imageTag            
        }
    }
}

# remove previous services & deployments
function Clean-Environment{
    Write-Host "Removing existing services & deployments.." -ForegroundColor Yellow
    Invoke-Expression 'kubectl delete deployments --all'
    Invoke-Expression 'kubectl delete services --all'
    Invoke-Expression 'kubectl delete pods --all'
}

# start deployment
function Deploy-Apps{
    Write-Host 'Deploying services' -ForegroundColor Yellow
    Invoke-Expression 'kubectl apply -f .'
}

# wait for ui pod to start and port forward port 80
function Port-Forward{
    Write-Host "Waiting for ui pod to start..." -ForegroundColor Yellow
    kubectl wait --for=condition=ready pod -l io.kompose.service=webapplicationcore-ui
    $uipod = Invoke-Expression 'kubectl get pods -l io.kompose.service=webapplicationcore-ui -o=name'

    Invoke-Expression "kubectl port-forward $uipod 8080:80"

    Write-Host "WebApplication is exposed at http://localhost:8080" -ForegroundColor Yellow
}

function Start-Deployment{
    Check-Commands

    Docker-Login

    Build-Images
    
    Push-Images

    Clean-Environment

    Deploy-Apps

    # Port-Forward
}

& Start-Deployment