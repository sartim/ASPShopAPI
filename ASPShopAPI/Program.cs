﻿using Microsoft.EntityFrameworkCore;
using ASPShopAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dotenv.net;

// Load .env
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db connection here
string connectionString = Environment.GetEnvironmentVariable("DB_URL");
builder.Services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(connectionString));


// Convert url structure to lower case
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your_issuer",
        ValidAudience = "your_audience",
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")))
    };
});

//services.AddScoped(typeof(BaseController<>));
//services.AddScoped<UserController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<TokenAuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

