# Contact Service -ASP.NET Core REST Web API 

This project contains an implementation of ASP.NET Core REST API with CRUD operations on a Contact table.

## About this Contact Service

- **Applies to:** SQL Server 2013 (or higher)
- **Tool:** Visual Studio 2017
- **Programming Language:** C#
- **Author:** Poonam Suryawanshi

<a name=before-you-begin></a>

## Before you begin

To run this Service, you need the following prerequisites.

**Software prerequisites:**

1. SQL Server 2013 (or higher) or an Azure SQL Database
2. Visual Studio 2017 (or higher) with the .NET Core 2.1

<a name=run-this-sample></a>

## Run this sample

### Setup

1.From SQL Server Management Studio connect to your SQL Server 2013(or higher) execute 
**DBSetupScript.sql** script that will create and populate Contact table in the database.


2.From Visual Studio 2017, open the **ContactServiceSolution.API.sln** file from the root directory. 
Restore packages using right-click menu on the project in Visual Studio and by choosing Restore Packages item. 
As an alternative, you may run **dotnet restore** from the command line (from the root folder of application).

3.Add a connection string in **appsettings.json or appsettings.development.json** file according to your database. An **example** of the content of appsettings.development.json is shown in the following configuration:

```
"ConnectionStrings": {

    "ContactDBConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ContactDatabase;Integrated Security=True;”
  }

```


### Build and run the REST services

1. Build solution using Ctrl+Shift+B, right-click on project + Build, Build/Build Solution from menu, or **dotnet build** 
command from the command line (from the root folder of application).

2. Run sample app using F5 or Ctrl+F5,
  1. Open swagger/index.html Url.You will see Swagger screen where all endpoints of services with their input payload will be displayed.
  Swagger is an open-source software framework backed by a large ecosystem of tools that helps developers design, build, document,
  and consume RESTful web services.
 
 Use swagger to test API. 


