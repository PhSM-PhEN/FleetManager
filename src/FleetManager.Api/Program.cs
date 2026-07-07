using FleetManager.Api.Filters;
using FleetManager.Application;
using FleetManager.Infrastructure;
using FleetManager.Infrastructure.Extension;
using FleetManager.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(opt => opt.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


if (builder.Configuration.IsTestEnviromment() == false)
{
    await MigrateDataBase();
}

app.Run();



async Task MigrateDataBase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);
}
public partial class Program { } // Needed for integration tests
