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

### .env.example

    ENV=Development
    PORT=5070
    JWT_SECRET_KEY=YourSuperSecretKeyHere
    JWT_ISSUER=your-issuer
    JWT_AUDIENCE=your-audience
    JWT_EXPIRY=300
    POSTGRES_USER=shopuser
    POSTGRES_PASSWORD=shoppassword
    POSTGRES_DB=shopdb
    DB_URL=Host=postgres;Port=5432;Database=shopdb;Username=shopuser;Password=shoppassword
