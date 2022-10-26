using Carpools.Controllers;
using TecAlliance.Carpool.Api;
using TecAlliance.Carpools.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TecAlliance.Carpools.Data.Models;
using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpools.Business.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TecAlliance.Carpools.Data.Services;

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
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddSingleton<CarpoolDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoProvider>();

builder.Services.AddSingleton<NonDriverDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<NonDriverDtoProvider>();

builder.Services.AddSingleton<DriverDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<DriverDtoProvider>();

builder.Services.AddScoped<INonDriverBusinessService, NonDriverBusinessService>();
builder.Services.AddScoped<INonDriverDataService, NonDriverDataService>();

builder.Services.AddTransient<IDriverBusinessService, DriverBusinessService>();
builder.Services.AddTransient<IDriverDataService, DriverDataService>();

builder.Services.AddTransient<ICarpoolBusinessService, CarpoolBusinessService>();
builder.Services.AddTransient<ICarpoolDataService, CarpoolDataService>();


var app = builder.Build();

// Configure the HTTP request pipeline.l
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();