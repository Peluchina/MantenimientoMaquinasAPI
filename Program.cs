using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Queries;
using MantenimientoMaquinasAPI.Application.Validators;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;
using MantenimientoMaquinasAPI.Infrastructure.Repository;
using MantenimientoMaquinasAPI.Application.Agents;
using MantenimientoMaquinasAPI.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MachinesAgentTools>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingresa tu token JWT. Ejemplo: Bearer eyJhbGci..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddSingleton<IConnectionSQL, ConnectionSqL>();
builder.Services.AddScoped<IMachinesRespositoryWR, MachinesRepository>();
builder.Services.AddScoped<MachinesListQueryHandler>();
builder.Services.AddScoped<MachinesGetQueryHandler>();
builder.Services.AddScoped<MachinesCreateCommandHandler>();
builder.Services.AddScoped<MachinesUpdateCommandHandler>();
builder.Services.AddScoped<MachinesDeleteCommandHandler>();
builder.Services.AddScoped<IValidator<MachinesCreateCommand>, MachinesCreateCommandValidator>();
builder.Services.AddScoped<IValidator<MachinesUpdateCommand>, MachinesUpdateCommandValidator>();
builder.Services.AddScoped<IValidator<MachinesDeleteCommand>, MachinesDeleteCommandValidator>();
builder.Services.AddScoped<IUsersRepositoryWR, UsersRepository>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<UsersListQueryHandler>();
builder.Services.AddScoped<UsersGetQueryHandler>();
builder.Services.AddScoped<UsersCreateCommandHandler>();
builder.Services.AddScoped<UsersUpdateCommandHandler>();
builder.Services.AddScoped<UsersDeleteCommandHandler>();
builder.Services.AddScoped<IValidator<UsersCreateCommand>, UsersCreateCommandValidator>();
builder.Services.AddScoped<IValidator<UsersUpdateCommand>, UsersUpdateCommandValidator>();
builder.Services.AddScoped<IValidator<UsersDeleteCommand>, UsersDeleteCommandValidator>();
builder.Services.AddScoped<IRolesRespositoryWR, RolesRepository>();
builder.Services.AddScoped<RolesListQueryHandler>();

builder.Services.AddScoped<IMaintenanceRecordsRespositoryWR, MaintenanceRecordsRepository>();
builder.Services.AddScoped<MaintenanceRecordsGetQueryHandler>();
builder.Services.AddScoped<MaintenanceRecordsListQueryHandler>();
builder.Services.AddScoped<MaintenanceRecordsCreateCommandHandler>();
builder.Services.AddScoped<IValidator<MaintenanceRecordsCreateCommand>, MaintenanceRecordsCreateCommandValidator>();

builder.Services.AddScoped<IMachineTemperatureHistoryRespositoryWR, MachineTemperatureHistoryRepository>();
builder.Services.AddScoped<MachineTemperatureHistoryGetQueryHandler>();
builder.Services.AddScoped<MachineTemperatureHistoryListQueryHandler>();
builder.Services.AddScoped<MachineTemperatureHistoryCreateCommandHandler>();
builder.Services.AddScoped<IValidator<MachineTemperatureHistoryCreateCommand>, MachineTemperatureHistoryCreateCommandValidator>();

// Configuración CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var jwtSecret = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrWhiteSpace(jwtSecret) || Encoding.UTF8.GetByteCount(jwtSecret) < 32)
{
    throw new InvalidOperationException("Falta configurar Jwt:SecretKey con una clave aleatoria de al menos 32 bytes.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = !string.IsNullOrWhiteSpace(builder.Configuration["Jwt:Issuer"]),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = !string.IsNullOrWhiteSpace(builder.Configuration["Jwt:Audience"]),
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

var azureEndpoint = builder.Configuration["AzureOpenAI:Endpoint"];
var azureDeployment = builder.Configuration["AzureOpenAI:Deployment"];
var azureApiKey = builder.Configuration["AzureOpenAI:ApiKey"];

if (string.IsNullOrWhiteSpace(azureEndpoint) ||
    string.IsNullOrWhiteSpace(azureDeployment) ||
    string.IsNullOrWhiteSpace(azureApiKey))
{
    throw new InvalidOperationException(
        "Falta configurar AzureOpenAI:Endpoint, " +
        "AzureOpenAI:Deployment o AzureOpenAI:ApiKey.");
}

var openAIClient = new OpenAIClient(
    new ApiKeyCredential(azureApiKey),
    new OpenAIClientOptions
    {
        Endpoint = new Uri(azureEndpoint)
    });

builder.Services.AddSingleton<IChatClient>(_ =>
    new FunctionInvokingChatClient(
        openAIClient
            .GetChatClient(azureDeployment)
            .AsIChatClient()));

builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<MachinesAgentTools>();

var app = builder.Build();
app.UseMiddleware<MachineExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
