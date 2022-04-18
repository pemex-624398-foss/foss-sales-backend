pipeline {
    agent {
        docker { 
            image 'mcr.microsoft.com/dotnet/sdk:6.0'
        }
    }
    
    stages {
        stage('Test') {
            steps {
                echo 'Testing...'
                sh 'dotnet --version'
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
            }
        }*/
    }
}