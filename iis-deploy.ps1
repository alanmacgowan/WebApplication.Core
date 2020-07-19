<#
.Synopsis
Script to build, deploy, test and package .net core web application.

.DESCRIPTION
Script to build, deploy, test and package .net core web application.

.EXAMPLE
./ii-deploy -Version "1.0.0.0" -Branch "master" -CleanEnvironment $true

#>

#Script ii-deploy
#Creator Alan Macgowan
#Date 07/19/2020
#Updated
#References


param(
    [Parameter(Mandatory=$true)][String]$Version,
    [String]$GitRepository = "https://github.com/alanmacgowan/WebApplication.Core.git",
    [String]$Branch = "master",
    [String]$PublishUrl_WEB = "C:\Jenkins_builds\sites\dev",
    [String]$PublishUrl_API = "C:\Jenkins_builds\sites\devAPI",
    [bool]$CleanEnvironment = $false
)

$SourcesFolder = $PSScriptRoot + "\sources"
$DateStamp = $((Get-Date).ToString("yyyyMMdd_HHmmss"))

Function Initialize-Directory{
    Write-Host "Checking directory exists" -ForegroundColor Green
    If (-Not(Test-Path $SourcesFolder)) {
        Write-Host "Creating directory SourcesFolder" -ForegroundColor Green
        New-Item -ItemType "directory" -Path  $SourcesFolder
    }
}

Function Get-SourceCode{
    Write-Host "Getting Source Code - Branch: $Branch" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core"
    If (Test-Path $SourcePath) {
        Set-Location $SourcePath
        git pull origin $Branch
    }
    Else {
        git clone -b $Branch $GitRepository $SourcePath
    }
}

Function Get-Packages{
    Write-Host "Getting Nuget packages" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core"
    Set-Location $SourcePath

    dotnet restore WebApplication.Core.sln
}

Function Build-Solution{
    Write-Host "Building solution" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core"
    Set-Location $SourcePath

    dotnet build WebApplication.Core.sln --no-restore --configuration Release /p:Version=$Version
}

Function Build-Webpack{
    Write-Host "Running Webpack" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core\WebApplication.Core.UI"
    Set-Location $SourcePath
    npm install
    npm run production
}

Function Run-Tests{
    Param($ProjectName)
    Write-Host "Running Tests $ProjectName" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core\$ProjectName\"
    Set-Location $SourcePath

    dotnet test $ProjectName.csproj
}

Function Deploy-Site{
    Param($SiteName, $PublishUrl)
    Write-Host "Deploying Site $SiteName" -ForegroundColor Green
    $SourcePath = $SourcesFolder + "\WebApplication.Core\$SiteName\"
    Set-Location $SourcePath

    dotnet publish $SiteName.csproj --no-build --configuration Release -o $PublishUrl
}

Function Smoke-Test{
    Param($SiteUrl)
    $Result = Invoke-WebRequest $SiteUrl
    If ($Result.StatusCode -ne 200) {
      Write-Error "Did not get 200 OK"
    } Else{
      Write-Host "Successfully connect." -ForegroundColor Green  
    }
}


Function Publish-Site{
    $ErrorActionPreference = 'Stop'
    #Try{
        Initialize-Directory

        Get-SourceCode
    
        Get-Packages
    
        Build-Webpack

        Build-Solution
    
        Run-Tests "WebApplication.Core.Tests.Unit"

        Deploy-Site "WebApplication.Core.UI" $PublishUrl_WEB

        Deploy-Site "WebApplication.Core.API" $PublishUrl_API

        # Smoke-Test "http://localhost:8090/"

        # Smoke-Test "http://localhost:4091/swagger/index.html"

        # Run-Tests "WebApplication.Core.Tests.Acceptance"
    <#}
    Catch{
        Write-Host "Error" -ForegroundColor Red
        Write-Host "Message: [$($_.Exception.Message)"] -ForegroundColor Red -BackgroundColor DarkBlue
    }
    Finally{#>
        Set-Location $PSScriptRoot
    #}
}

& Publish-Site