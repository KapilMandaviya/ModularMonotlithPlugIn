
using DataContextLibr.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserContract;


namespace ReloadModuleDynamic
{
    //public class ReloadModuleServices :IReloadModuleServices
    //{
    //    private readonly ModuleLoader _loader;
    //    private readonly ModularMonolithPluginContext _db;
    //    private readonly IConfiguration _config;
    //    private readonly IServiceCollection _services;

    //    public ReloadModuleServices(ModuleLoader loader, ModularMonolithPluginContext db, IConfiguration config, IServiceCollection services)
    //    {
    //        _loader = loader;
    //        _db = db;
    //        _config = config;
    //        _services = services;
    //    }

    //    public void DynamicReloadModule()
    //    {
    //        var enabledModules = _db.Modules.Where(m => m.IsEnabled == true).ToList();
    //        foreach (var module in enabledModules)
    //        {
    //            var fullPath = Path.GetFullPath(module.DllPath);
    //            _loader.LoadModule(fullPath, _services, _config);
    //        }
    //    }
    //}


    public class ReloadModuleService : IReloadModuleServices
    {
        //private readonly PluginManager _pluginManager;

        //public ReloadModuleService(PluginManager  pluginManager)
        //{
        //    _pluginManager = pluginManager;
        //}
        //private readonly ModuleLoader _loader;

        //private readonly IServiceProvider _provider;
        //private readonly IConfiguration _config;

        //public ReloadModuleService(ModuleLoader loader, IServiceProvider provider, IConfiguration config, PluginManager pluginManager)
        //{
        //    _loader = loader;
        //    _provider = provider;
        //    _config = config;
        //    _pluginManager = pluginManager;

        //}

        public void DynamicReloadModule()
        {
            //using var scope = _provider.CreateScope();
            //var db = scope.ServiceProvider.GetRequiredService<ModularMonolithPluginContext>();

            //var enabledModules = db.Modules.Where(m => m.IsEnabled==true).ToList();
            //foreach (var module in enabledModules)
            //{
            //    var fullPath = Path.GetFullPath(module.DllPath);
            //    _loader.LoadModule(fullPath, services: null, _config); // will fix `services` below
            //}
            //_pluginManager.ReloadModules();

        }
    }

}
