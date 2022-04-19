pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:6.0'
            // image 'alpine:3.15.4'
            reuseNode true
        }
    }
    
    stages {
        stage('Test') {
            steps {
                echo 'Testing 1...'
                echo 'Testing 2...'
                echo 'Testing 3...'
                sh 'ls -lha'
                // sh 'dotnet --version'
            }
        }
        /*stage('Build') {
            steps {
                echo 'Building...'
                
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
        stage('Deploy') {
            steps {
                echo 'Deploying...'
                sh '''export CR_PAT=YOUR_TOKEN
                echo $CR_PAT | docker login ghcr.io -u adrian8167e --password-stdin
                docker build -t ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest .
                docker push ghcr.io/pemex-624398-foss/foss-sales-backend:624398-latest
                '''
            }
        }*/
    }
}
