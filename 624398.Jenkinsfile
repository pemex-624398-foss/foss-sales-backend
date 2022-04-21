pipeline {
    agent any
    stages {
        stage('Build') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:6.0'
                    args '--network host'
                    reuseNode true
                }
            }
            steps {
                echo 'Building =============================='
                
                sh 'pwd'
                sh 'ls -lha'
                
                // Restore solution dependencies
                sh 'dotnet restore'
                
                // Build application in Debug configuration
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"'
            }
        }
        stage('Test') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:6.0'
                    args '--network host'
                    reuseNode true
                }
            }
            steps {
                echo 'Testing =============================='
                
                // Run tests
                // sh 'export ASPNETCORE_ENVIRONMENT=Staging && dotnet test'
            }
        }
        stage('Publish') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:6.0'
                    args '--network host'
                    reuseNode true
                }
            }
            steps {
                echo "Publishing =============================="
                
                // Build application in Release configuration
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release'
                
                // Publish application
                sh 'dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release'
             
                // List published files
                sh 'ls -lh Foss.Sales.Backend.Api/bin/Release/net6.0/publish/'
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying =============================='
                
                // Build docker image
                sh 'docker build -t adrian8167e/foss-sales-backend:624398-latest Foss.Sales.Backend.Api'
                
                // Push docker image
                sh '''cat docker-pat.txt | docker login -u $DOCKER_HUB_USER --password-stdin               
                docker push adrian8167e/foss-sales-backend:624398-latest'''
                
                // List docker images
                sh 'docker images'                
            }
        }
    }
    
    post {
        always {
            echo 'Run the steps in the post section regardless of the completion status of the Pipeline’s or stage’s run.'
        }
        
        changed {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has a different completion status from its previous run.'
        }
        
        fixed {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run is successful and the previous run failed or was unstable.'
        }
        
        regression {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run’s status is failure, unstable, or aborted and the previous run was successful.'
        }
        
        aborted {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has an "aborted" status, usually due to the Pipeline being manually aborted. This is typically denoted by gray in the web UI.'
        }
        
        failure {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has a "failed" status, typically denoted by red in the web UI.'
        }
        
        success {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has a "success" status, typically denoted by blue or green in the web UI.'
        }
        
        unstable {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has an "unstable" status, usually caused by test failures, code violations, etc. This is typically denoted by yellow in the web UI.'
        }
        
        unsuccessful {
            echo 'Only run the steps in post if the current Pipeline’s or stage’s run has not a "success" status. This is typically denoted in the web UI depending on the status previously mentioned.'
        }
        
        cleanup {
            echo 'Run the steps in this post condition after every other post condition has been evaluated, regardless of the Pipeline or stage’s status.'
        }
    }
}
