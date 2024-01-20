using VisitorSecurityClearanceSystemAPI.Common;
using VisitorSecurityClearanceSystemAPI.CosmosDBServer;
using VisitorSecurityClearanceSystemAPI.Interfaces;
using VisitorSecurityClearanceSystemAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add dependency in program.cs
builder.Services.AddSingleton<IManagerService, ManagerService>();
builder.Services.AddSingleton<ICosmosService, CosmosService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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

app.Run();
