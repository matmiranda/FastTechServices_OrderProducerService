﻿using OrderProducerService.Application.Interfaces;
using OrderProducerService.Domain.Settings;
using OrderProducerService.Infrastructure.MessageBroker;
using OrderProducerService.Infrastructure.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o serviço de health check
builder.Services.AddHealthChecks();

// JWT Settings (caso queira configurar futuramente um validador próprio aqui)
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);

// Registrar o AuthClient com HttpClient para validação do token via API externa
builder.Services.AddHttpClient<IAuthClient, AuthClient>();

// RabbitMQ Producer
builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

// Application Service
builder.Services.AddScoped<IOrderProducerService, OrderProducerService.Application.Services.OrderProducerService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Menu Producer API", Version = "v1" });

    c.EnableAnnotations(); // Habilita [SwaggerOperation], etc.

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Informe o token JWT no formato: Bearer {seu_token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Mapeia o endpoint de health check
app.MapHealthChecks("/order-producer/health");


app.UseSwagger();
app.UseSwaggerUI();

// Adicionar middleware do Prometheus com endpoint customizado
app.UseMetricServer("/order-producer/metrics");
app.UseHttpMetrics();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
