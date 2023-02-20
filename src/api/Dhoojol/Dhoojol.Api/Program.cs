using Dhoojol.Application.Extensions;
using Dhoojol.Infrastructure.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .Build();

builder.Services.AddControllers();

// Ajout du swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DhoojolApi", Version = "v1" });
});

builder.Services.AddInfrastructureImplementations(configuration);
builder.Services.AddApplicationImplementations(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(b => b
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapControllers();

await app.MigrateDbAsync();

app.Run();


