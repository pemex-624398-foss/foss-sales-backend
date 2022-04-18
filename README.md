# FOSS Sales Backend
Aplicación de ejemplo para demostración de tareas de CI/CD con Jenkins.


## Requisitos
* Terminal de Línea de Comandos
* [Git](https://git-scm.com/)
* [Docker](https://www.docker.com/)
* [.NET SDK 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
* Editor de Texto / IDE


## Preparación
Ejecutar servidor PostgreSQL y crear bases de datos.
````shell
# 1. Descargar Imágenes Docker
docker pull postgres:14.2
docker pull adminer:4.8.1

# 2. Crear Volúmenes
docker volume create foss-sales-backend_postgres-data

# 3. Iniciar Servicios
docker run -d --name foss-postgres -e POSTGRES_PASSWORD=p0stgr3s --network foss -p 5432:5432 -v foss-sales-backend_postgres-data:/var/lib/postgresql/data postgres:14.2
docker run -d --name foss-adminer --network foss -p 9090:8080 adminer:4.8.1

# 4. Crear bases de datos
docker exec foss-postgres createdb -U postgres -E UTF8 foss_sales_dev
docker exec foss-postgres createdb -U postgres -E UTF8 foss_sales_qa
docker exec foss-postgres createdb -U postgres -E UTF8 foss_sales_prod

# 5. Aplicar migraciones (copiar y personalizar Foss.Sales.Backend.Api/Infrastructure/Sql/Migrations/args.Template.txt)
dotnet tool restore
dotnet tool run evolve -- migrate '@Foss.Sales.Backend.Api/Infrastructure/Sql/Migrations/args.Development.txt'
dotnet tool run evolve -- migrate '@Foss.Sales.Backend.Api/Infrastructure/Sql/Migrations/args.Staging.txt'
dotnet tool run evolve -- migrate '@Foss.Sales.Backend.Api/Infrastructure/Sql/Migrations/args.Production.txt'
````

## Jenkinsfile
```shell
# Crear una nueva rama <ficha>-prod basada en develop 
git checkout -b 123456-prod develop

# Copiar y personalizar el archivo Jenkinsfile.Template como <ficha>.Jenkinsfile

# Editar archivos del proyecto y publicar cambios
git add .
git commit -m "my commit message"
git push origin 123456-prod
``` 
