# ASP.NET Web API

REST API using ASP.NET C#

## Setup

### Install Entity Framework Core tools

    $ dotnet tool install --global dotnet-ef

### Create migrations

    $ dotnet ef migrations add <name>


### Update database

    $ dotnet ef database update

### Build

    $ dotnet build

### Run

    $ dotnet watch run -- --logging:LogLevel:Default=Debug
