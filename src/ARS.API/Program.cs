using ARS.API;
using ARS.Web;

var builder = WebApplication.CreateBuilder(args);

//Needs to be called before services so assemblies gets loaded into AppDomain
ModuleHandler.Initialize();

builder.Services.AddOptions();
//Register ARS.Web services
builder.Services.RegisterServices(false);

//Map controllers from modules
var controllers = builder.Services.AddControllers();

foreach (var module in ModuleHandler.Modules)
{
    controllers.AddApplicationPart(module.NavMenu.Assembly).AddControllersAsServices();
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

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