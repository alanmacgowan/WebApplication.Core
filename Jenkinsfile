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
				stage('Restore dependencies'){
					parallel {
        				stage('Restore packages'){
                           steps{
                              bat "dotnet restore WebApplication.Core.sln"
                            }
                        }
                		stage('NodeJS'){
        					steps{
        						nodejs(nodeJSInstallationName: 'Node') {
        							dir('WebApplication.Core.UI')
        							{
        								bat 'npm install && npm run production'
        							}
        						}
        					}
        				}
					}
				}	
                stage('Build'){
                   steps{
                      bat "dotnet build WebApplication.Core.sln --no-restore --configuration Release /p:Version=${BUILD_NUMBER}"
                    }
                }
                stage('Test: Unit Test'){
                   steps {
                     bat "dotnet test WebApplication.Core.Tests.Unit\\WebApplication.Core.Tests.Unit.csproj"
                    }
                }
                stage('Publish QA'){
					parallel {
                        stage('Publish API: QA'){
                             steps{
                               bat "dotnet publish WebApplication.Core.API\\WebApplication.Core.API.csproj --no-build --configuration Release -o C:\\Jenkins_builds\\sites\\devAPI"
                            }
                        }
                        stage('Publish UI: QA'){
                             steps{
                               bat "dotnet publish WebApplication.Core.UI\\WebApplication.Core.UI.csproj --no-build --configuration Release -o C:\\Jenkins_builds\\sites\\dev"
                            }
                        }
					}
                }	
                stage('Smoke Test QA') {
					steps {
						smokeTest("http://localhost:8090/")
						smokeTest("http://localhost:4091/swagger/index.html")
					}
				}
                stage('Run Tests'){
					parallel {
        				stage('Test: Integration Test'){
                            steps {
                                echo "Test: Integration Test"
                               //bat "dotnet test WebApplication.Core.Tests.Integration\\WebApplication.Core.Tests.Integration.csproj"
                            }
                        }
        				stage('Test: Acceptance test') {
        				    steps {
        				        dir('WebApplication.Core.Tests.Acceptance\\bin\\Release')
                                {
                                    //bat "dotnet test WebApplication.Core.Tests.Acceptance\\WebApplication.Core.Tests.Acceptance.csproj"
                                   
                                    //bat "\"${VSTest}\" \"WebApplication.Tests.Acceptance.dll\" /Logger:trx;LogFileName=Results_${env.BUILD_NUMBER}.trx /Framework:Framework45"
                                }
                                //step([$class: 'MSTestPublisher', testResultsFile:"**/*.trx", failOnError: true, keepLongStdio: true])
                            }
                        }                        
        				stage('Test: Performance Test'){
                            steps {
                                echo "Test: Performance Test"
                            }
                        }
        				stage('Test: API Test'){
                            steps {
                                echo "Test: API Test"
                            }
                        }                        
					}
                }	
			}	
}

void smokeTest(String url){
    def status = powershell (returnStatus: true, script: """
		\$result = Invoke-WebRequest $url
		if (\$result.StatusCode -ne 200) {
			Write-Error \"Did not get 200 OK\"
			exit 1
		} else{
			Write-Host \"Successfully connect.\"
		}
    """)
    if (status != 0) {
       error "Smoke test failed: $url"
    }
}
				