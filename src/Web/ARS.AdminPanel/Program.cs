using ARS.AdminPanel;
using ARS.Web;

var builder = WebApplication.CreateBuilder(args);

//Needs to be called before services so assemblies gets loaded into AppDomain
ModuleHandler.Initialize();

builder.Services.AddOptions();
//Register ARS.Web services
builder.Services.RegisterServices();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

//Disposes the database and features from ARS.Web
SetupHelper.Dispose();