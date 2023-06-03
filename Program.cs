using Microsoft.EntityFrameworkCore;
using Places.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IGooglePlacesService, GooglePlacesService>();

builder.Services.AddControllers();
builder.Services.AddCors(policy => 
    policy.AddPolicy("OpenCorsPolicy", opt => 
        opt.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod()
    )
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

// Database Context Dependancy Injection 

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME"); ;
var dbPassword = Environment.GetEnvironmentVariable("DB_MSSQL_SA_PASSWORD"); ;
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True";
//connectionString = $"Server=localhost,8082;Initial Catalog=PlacesApp;User ID=sa;Password=Password.123;TrustServerCertificate=True";

builder.Services.AddDbContext<ApiDbContext>(opt => opt.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
}

app.UseCors("OpenCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
 