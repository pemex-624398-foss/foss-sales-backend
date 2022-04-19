pipeline {
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
                
                sh 'pwd'
                sh 'ls -lha'
                sh 'dotnet restore'
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"'
            }
        }
        stage('Test') {
            agent {
                docker { 
                    image 'mcr.microsoft.com/dotnet/sdk:6.0'
                    args '--network host'
                }
            }
            steps {
                echo 'Testing...'
                sh 'export ASPNETCORE_ENVIRONMENT=Staging && dotnet test'
            }
        }
        stage('Deploy') {
            agent any
            steps {
                echo 'Deploying...'
                sh 'docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest .'
                sh 'docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest'
                
                // sh '''export CR_PAT=YOUR_TOKEN
                // echo $CR_PAT | docker login ghcr.io -u adrian8167e --password-stdin
                // docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest .
                // docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest
                // '''
            }
        }
    }
}
