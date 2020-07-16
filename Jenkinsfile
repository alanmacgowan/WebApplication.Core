pipeline {
			agent any
			environment{
				RELEASE_VERSION = "1.0.1"
            }
			options {
				buildDiscarder(logRotator(numToKeepStr:'5'))
			}
			stages {
				stage('Get Source'){
					steps{
						checkout([$class: 'GitSCM', branches: [[name: 'master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [], userRemoteConfigs: [[credentialsId: 'github', url: 'https://github.com/alanmacgowan/WebApplication.Core.git']]])
					}
				}
				stage('Restore packages'){
                   steps{
                      bat "dotnet restore WebApplication.Core.sln"
                    }
                }
                stage('Clean'){
                    steps{
                        bat "dotnet clean WebApplication.Core.sln"
                    }
                } 
                stage('Build'){
                   steps{
                      bat "dotnet build WebApplication.Core.sln --configuration Release /p:Version=${BUILD_NUMBER}"
                    }
                }
                stage('Test: Unit Test'){
                   steps {
                     bat "dotnet test WebApplication.Core.Tests.Unit\\WebApplication.Core.Tests.Unit.csproj"
                    }
                }
                stage('Test: Integration Test'){
                    steps {
                        echo "Test: Integration Test"
                       //bat "dotnet test WebApplication.Core.Tests.Integration\\WebApplication.Core.Tests.Integration.csproj"
                    }
                }
                stage('Publish'){
                     steps{
                       bat "dotnet publish WebApplication.Core.API\\WebApplication.Core.API.csproj "
                    }
                }
			}	
}				
				