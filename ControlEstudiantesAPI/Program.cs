using ControlEscolarCore.Controller;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<EstudiantesController>();

// Configura manualmente la cadena de conexión para que ConfigurationManager pueda encontrarla
builder.Configuration["ConexionBD"] = builder.Configuration.GetConnectionString("DefaultConnection");

// Solo necesitamos esta línea, eliminando las variables intermedias que causan ambigüedad
System.Configuration.ConfigurationManager.ConnectionStrings.Add(
    new ConnectionStringSettings("ConexionBD",
    builder.Configuration.GetConnectionString("DefaultConnection")));

System.Configuration.ConfigurationManager.ConnectionStrings.Add(
    new ConnectionStringSettings("ConexionBD",
    builder.Configuration.GetConnectionString("DefaultConnection")));

// En Program.cs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();