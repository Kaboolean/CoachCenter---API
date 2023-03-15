using Dhoojol.Application.Extensions;
using Dhoojol.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;

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
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "bearer {token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    c.SwaggerDoc("v1", new() { Title = "DhoojolApi", Version = "v1" });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Auth:Token"]))
        };
    });

builder.Services.AddInfrastructureImplementations(configuration);
builder.Services.AddApplicationImplementations(configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(b => b
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapControllers();

await app.MigrateDbAsync();

app.Run();


