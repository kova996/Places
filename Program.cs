using Microsoft.EntityFrameworkCore;
using Places.Contracts.Repository;
using Places.Data;
using Places.Extensions;
using Places.Hubs;
using Places.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddSingleton<IFourSquarePlacesService, FourSquarePlacesService>();

builder.Services.AddControllers();

builder.Services.RegisterCors();

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterHttpClient(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
   builder.Services.RegisterDbContext(builder.Configuration);
}

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

app.MapHub<EventsHub>("/eventsHub");

app.Run();
