pipeline {
    agent any
    stages {
        stage('Build') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:6.0'
                    args '--network host'
                }
            }
            steps {
                echo 'Building...'
              
                sh 'ls -lh'
                echo 'docker ps -a'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
              
                sh 'ls -lh'
                echo 'docker ps -a'
            }
        }
        /*stage('Build') {
            steps {
                echo 'Building...'
                
                sh 'pwd'
                sh 'ls -lha'
                sh 'dotnet restore'
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'export ASPNETCORE_ENVIRONMENT=Staging && dotnet test'
            }
        }
        stage('Publish') {
            steps {
                echo "Publishing..."
                
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release'
                sh 'dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release'
            }
        }
        stage('Deploy') {
            steps {
                node {
                    echo 'Deploying...'
          
                    echo 'TODO: Build docker image'
                    echo 'TODO: Push docker image'
                    
                    sh 'ls -lh Foss.Sales.Backend.Api/bin/Release/net6.0/publish/'
                    echo 'docker ps -a'
                    
                    // sh '''export CR_PAT=YOUR_TOKEN
                    // echo $CR_PAT | docker login ghcr.io -u adrian8167e --password-stdin
                    // docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest .
                    // docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest
                    // '''
                }
            }
        }*/
    }
}
