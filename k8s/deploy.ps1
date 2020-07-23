#./deploy.ps1 -dockerUser User -dockerPassword SecretPassword 

Param(
    [parameter(Mandatory=$false)][string]$dockerUser,
    [parameter(Mandatory=$false)][string]$dockerPassword,
    [parameter(Mandatory=$false)][string]$imageTag="latest",
    [parameter(Mandatory=$false)][bool]$buildImages=$true,
    [parameter(Mandatory=$false)][bool]$pushImages=$true,
    [parameter(Mandatory=$false)][string]$dockerOrg="alanmacgowan"
)

# check required commands 
$requiredCommands = ("docker", "docker-compose", "kubectl")
foreach ($command in $requiredCommands) {
    if ((Get-Command $command -ErrorAction SilentlyContinue) -eq $null) {
        Write-Host "$command must be on path" -ForegroundColor Red
        exit
    }
}

# login to dockerhub
if (-not [string]::IsNullOrEmpty($dockerUser)) {
    Write-Host "Logging in to dockerhub as user $dockerUser" -ForegroundColor Yellow
    docker login -u $dockerUser -p $dockerPassword
    
    if (-not $LastExitCode -eq 0) {
        Write-Host "Login failed" -ForegroundColor Red
        exit
    }
}

Write-Host "Docker image Tag: $imageTag" -ForegroundColor Yellow

# building  docker images if needed
if ($buildImages) {
    Write-Host "Building Docker images tagged with '$imageTag'" -ForegroundColor Yellow
    $env:TAG=$imageTag
    docker-compose -p .. -f ../docker-compose.yml -f ../docker-compose.production.yml build    
}

# push images to dockerhub
if ($pushImages) {
    Write-Host "Pushing images to $registry/$dockerOrg..." -ForegroundColor Yellow
    $services = ("webapplication.core.ui", "webapplication.core.api")
    foreach ($service in $services) {
        $imageFqdn = "$dockerOrg/${service}"
        docker tag ${service}:$imageTag ${imageFqdn}:$imageTag
        docker push ${imageFqdn}:$imageTag            
    }
}

# remove previous services & deployments
Write-Host "Removing existing services & deployments.." -ForegroundColor Yellow
Invoke-Expression 'kubectl delete deployments --all'
Invoke-Expression 'kubectl delete services --all'

# start deployment
Write-Host 'Deploying services' -ForegroundColor Yellow
Invoke-Expression 'kubectl apply -f .'

#wait for ui pod to start and port forward port 80
Write-Host "Waiting for ui pod to start..." -ForegroundColor Yellow
kubectl wait --for=condition=ready pod -l io.kompose.service=webapplicationcore-ui
$uipod = Invoke-Expression 'kubectl get pods -l io.kompose.service=webapplicationcore-ui -o=name'

Invoke-Expression "kubectl port-forward $uipod 8080:80"

Write-Host "WebApplication is exposed at http://localhost:8080" -ForegroundColor Yellow
