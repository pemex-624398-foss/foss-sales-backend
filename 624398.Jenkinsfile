pipeline {
    agent {
        docker { image 'mcr.microsoft.com/dotnet/sdk:6.0' }
    }
    
    stages {
        stage('Build') {
            steps {
                echo 'Building...'
                sh 'dotnet restore'
                sh 'dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release -o /app/build'
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
        }
    }
}