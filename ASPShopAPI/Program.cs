using Microsoft.EntityFrameworkCore;
using ASPShopAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dotenv.net;

// Load .env
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Set port for web server
int port = Convert.ToInt32(Environment.GetEnvironmentVariable("PORT"));
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(port);
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db connection here
var connectionString = Environment.GetEnvironmentVariable("DB_URL");
if (string.IsNullOrEmpty(connectionString))
    throw new Exception("DB_URL environment variable is not set.");

builder.Services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(connectionString));

// Convert url structure to lower case
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// JWT authentication setup
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") 
                ?? throw new Exception("JWT_SECRET_KEY not set.");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") 
                ?? throw new Exception("JWT_ISSUER not set.");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") 
                  ?? throw new Exception("JWT_AUDIENCE not set.");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
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

// Skips auth check
app.UseWhen(context => 
    !context.Request.Path.StartsWithSegments("/api/v1/auth/generate-jwt") &&
    !context.Request.Path.StartsWithSegments("/api/v1/health"),
appBuilder =>
{
    appBuilder.UseMiddleware<TokenAuthenticationMiddleware>();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

