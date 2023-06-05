using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IFourSquarePlacesService, FourSquarePlacesService>();

builder.Services.AddControllers();
builder.Services.AddCors(policy =>
    policy.AddPolicy("OpenCorsPolicy", opt =>
        opt.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod()
    )
);

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("foursquare", c =>
{
    c.BaseAddress = new Uri("https://api.foursquare.com");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
    c.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", builder.Configuration.GetValue<string>("FourSquareApiKey"));
});


if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<RequestResponseDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
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
