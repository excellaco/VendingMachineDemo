# This repository is now archived.

For future updates and to follow progress, please see:

https://github.com/excellatraining/VendingMachineDemo

# Welcome to the Excella Vending Machine demo!
This demo shows a working example of a small project with unit tests at each level.

# How to Run this Application

## Prerequisites
To run the project, you'll need to have some basic items set up or installed.

* **Internet.** You'll need an internet connection so that you can restore nuget packages.
* **.NET Core SDK.** You'll need the latest version of the .NET Core SDK. 3.1 should suffice.
* **Visual Studio or similar IDE`**. VSCode should work fine.
* **SQL Server Express**. The acceptance tests and web application use a database, which this example assumes is a SQL Server database.

Using Chocolatey (<http://chocolatey.org>) could be helpful in installing these prerequisites, e.g. then you can run: 

* `choco install MsSqlServer2014Express` to install SQL Server express.
 
## Getting Started

### Adding the Selenium Chrome Driver to the PATH variable.

If you used chocolatey, this won't be necessary. Otherwise, edit your %PATH% and add a reference to the directory that contains your downloaded chromedriver.exe. 

### Running the Migration to Deploy the Database

* Open the project in Visual Studio
* Open the package management console
* Select `Excella.Vending.DAL` as the default project
* In the package management console, type `Update-Database` and run.

This should create the database and run the initial migration to set everything up.

### Adding the Initial Payment row to the Database

This is performed by the initial migration, but you can run the following SQL if there is no row in the `Payments` table.

```
  SET IDENTITY_INSERT dbo.Payment ON
  INSERT INTO dbo.Payment
    (ID, Value)
  VALUES 
    (1, 0)
  SET IDENTITY_INSERT dbo.Payment OFF
```

## Building the Application

You can build via Visual Studio if you'd like.

You can also open the root of the project in a comman prompt and then run `dotnet build`

## Deploying the Application

You don't need IIS to run the application.

After building, open a command prompt in the root of the repository, and run:

`dotnet run --project Excella.Vending.Web.UI`

This will start a web application on ports `5000` and `5000`.

## Running the tests

You should be able to run tests in the test runner of your choice -- Visual Studio, ReSharper, NCrunch, etc.

## :warning: Known Issues
* `ChromeDriver.exe` is still not cleaned up correctly. Instances remain around even after test execution. You may want to quickly run `taskkill /im chromedriver.exe /F` when finished with tests.

## :warning: Recently Resolved Issues

* IIS Express setup was gnarly. We moved the project to .NET Core to avoid some of the platform / machine specific issues.
* AATs had to be run using Debugging rather than just executing the tests. This no longer appears to be an issue.

### :warning: KNOWN ISSUE: Currently the Chrome-based AATs require Debugging

For some reason, the IIS Express setup appears to refuse connections unless the application is being actively debugged. So rather than running those tests, you may need to "Debug" those tests in order to get them to pass successfully.
