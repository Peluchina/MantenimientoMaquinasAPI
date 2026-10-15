using FluentValidation;
using MantenimientoMaquinasAPI.Application.Commands;
using MantenimientoMaquinasAPI.Application.Queries;
using MantenimientoMaquinasAPI.Application.Validators;
using MantenimientoMaquinasAPI.Domain.Contracts;
using MantenimientoMaquinasAPI.Infrastructure.Data;
using MantenimientoMaquinasAPI.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Services.AddScoped<UsersListQueryHandler>();
builder.Services.AddScoped<UsersGetQueryHandler>();
builder.Services.AddScoped<UsersCreateCommandHandler>();
builder.Services.AddScoped<UsersUpdateCommandHandler>();
builder.Services.AddScoped<UsersDeleteCommandHandler>();
builder.Services.AddScoped<IValidator<UsersCreateCommand>, UsersCreateCommandValidator>();
builder.Services.AddScoped<IValidator<UsersUpdateCommand>, UsersUpdateCommandValidator>();
builder.Services.AddScoped<IValidator<UsersDeleteCommand>, UsersDeleteCommandValidator>();

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


var app = builder.Build();
app.UseMiddleware<MachineExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
