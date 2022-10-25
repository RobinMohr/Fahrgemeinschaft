using Carpools.Controllers;
using TecAlliance.Carpool.Api;
using TecAlliance.Carpools.Business.Services;
using Microsoft.EntityFrameworkCore;
using TecAlliance.Carpools.Data.Models;
using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpools.Business.Models;

Files filemanager = new Files();
filemanager.CheckForAllCsvFiles();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});

builder.Services.AddSingleton<CarpoolDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoProvider>();

builder.Services.AddSingleton<NonDriverDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<NonDriverDtoProvider>();

builder.Services.AddSingleton<DriverDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<DriverDtoProvider>();

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