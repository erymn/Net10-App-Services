# Net10-App-Services

## Setup

```bash
dotnet new sln --name Net10-App-Services
dotnet new console --name Net10-App-Services.Console --output src/Net10-App-Services.Console --no-gitignore
dotnet sln add src/Net10-App-Services.Console
```

```bash
Setup Directory.Packages.props
Setup nuget.config
Add sql-scripts folder
```

## Pulling the SQL Server Database 2025
Because SQL Server image run at 64bit, you can use Azure SQL Edge, but the Azure SQL Edge does not supported by Microsoft, but you still can use it.

```bash
docker pull mcr.microsoft.com/mssql/server:2025-latest
```

## Running the SQL Server Database 2025
```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrongPassword123!" -p 1433:1433 --name sqlserver2025 -d mcr.microsoft.com/mssql/server:2025-latest
```

```bash
docker pull mcr.microsoft.com/azure-sql-edge:latest
```

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrongPassword123!" -p 1433:1433 --name sqlserver2025 -d mcr.microsoft.com/azure-sql-edge:latest
```

## Create Northwind Entity Models
Create ModernApps solutions and add Northwind.EntityModels

Steps to do:
1. Build the project
    ```bash
    dotnet build
    ```
2. Do Database Scaffold to generate Entity Class from table(s)
    ```bash
    dotnet ef dbcontext scaffold "Data Source=[Database Server];Initial Catalog=Northwind;User Id=[secret_user];Password=[secret_password];TrustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer --namespace Northwind.EntityModels --data-annotations
    ```
    The command to perform: `dotnet ef dbcontext scaffold`
    The connection string: "Data Source=tcp:127.0.0.1,1433;Initial Catalog=Northwind;User Id=sa;Password= s3cret-Ninja;TrustServerCertificate=true;"
    The database provider: `Microsoft.EntityFrameworkCore.SqlServer`
    The namespace: `--namespace Northwind.EntityModels`
    To use data annotations as well as the Fluent API: `--data-annotations`
