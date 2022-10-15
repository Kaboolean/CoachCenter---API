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

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.MigrateDbAsync();

app.Run();


