pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        sh '''pwd
ls -lha
dotnet restore
dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj"'''
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
        sh 'export ASPNETCORE_ENVIRONMENT=Staging && dotnet test'
      }
    }

    stage('Publish') {
      steps {
        sh '''dotnet build "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release
dotnet publish "Foss.Sales.Backend.Api/Foss.Sales.Backend.Api.csproj" -c Release
ls -lha Foss.Sales.Backend.Api/bin/Release/net6.0/publish/
'''
      }
    }

    stage('Deploy') {
      steps {
        sh '''docker build -t adrian8167e/foss-sales-backend:624398-latest Foss.Sales.Backend.Api
cat docker-pat.txt | docker login -u $DOCKER_HUB_USER --password-stdin
docker push adrian8167e/foss-sales-backend:624398-latest
docker images'''
      }
    }

  }
}