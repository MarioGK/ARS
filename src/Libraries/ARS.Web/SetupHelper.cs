using ARS.Common.Helpers;
using ARS.Common.Interfaces;
using ARS.Web.AutoFields;
using ARS.Web.Helpers;
using ARS.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace ARS.Web;

public static class SetupHelper
{
    public static void Dispose()
    {
        
    }
    
    /*public static async Task SeedDatabase(IDocumentStore documentStore)
    {
        var baseComponentType = typeof(IDatabaseSeeder);
        var allTypes = TypesHelper.AllTypes;
        Console.WriteLine($"Found {allTypes.Count} types in assemblies");

        var databaseSeedersTypes = allTypes
            .Where(x => baseComponentType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        
        var databaseSeeders = databaseSeedersTypes
            .Select(x => (IDatabaseSeeder)Activator.CreateInstance(x)!).ToList();

        foreach (var seeder in databaseSeeders)
        {
            await seeder.Seed(documentStore);
        }

        Console.WriteLine($"Found {databaseSeeders.Count} database seeders");
        
        Console.WriteLine("Finished database seeder async task!");
    }*/

    public static IServiceCollection RegisterServices(this IServiceCollection services, bool ui = true)
    {
        var database = DatabaseHelpers.StartDatabase();
        
        //SeedDatabase
        //Console.WriteLine("Starting database seeder async task ...");
        //Task.Run(async () => await SeedDatabase(documentStore));
        
        services.AddSingleton(database);
        services.RegisterCollections();

        if (ui)
        {
            //Start AutoField Builders
            AutoFieldBuilders.Create();
            
            //MudBlazor
            services.AddMudServices(config =>
            {
                //Snackbar
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 5000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
        
            //Blazored
            services.AddBlazoredLocalStorage();
            
            //Custom Services
            services.AddScoped<AutoDialogService>();
            services.AddScoped<AutoTableItemAddedService>();
            services.AddSingleton<GenericStateChangeService>();
        }

        //Authorization
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        return services;
    }

    private static void RegisterCollections(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.Scan(scan =>
        {
            scan.FromAssemblies(assemblies).AddClasses(classes => classes.AssignableTo<IBaseCollection>()).AsSelf().WithSingletonLifetime();
        });
    }
}