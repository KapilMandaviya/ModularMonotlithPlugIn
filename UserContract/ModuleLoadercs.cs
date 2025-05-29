using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using UserContract.UserContract;

namespace UserContract
{
    // ========== MainApp: Loader/ModuleLoader.cs ==========
    public class ModuleLoader
    {
        private readonly Dictionary<string, (PluginLoadContext Context, Assembly Assembly)> _loadedModules = new();

        public void LoadModule(string modulePath, IServiceCollection services, IConfiguration config)
        {
            if (!File.Exists(modulePath)) return;

            if (_loadedModules.ContainsKey(modulePath)) return; // already loaded
            string absolutePath = Path.GetFullPath(modulePath);

            var context = new PluginLoadContext(absolutePath);
            var assembly = context.LoadFromAssemblyPath(absolutePath);


            var initializerType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (initializerType == null)
                throw new ArgumentNullException(nameof(initializerType), "Initializer type cannot be null.");

            if (!typeof(IModuleInitializer).IsAssignableFrom(initializerType))
                throw new ArgumentException($"Type {initializerType.FullName} does not implement IModuleInitializer.", nameof(initializerType));

            if (Activator.CreateInstance(initializerType) is not IModuleInitializer initializer)
                throw new InvalidOperationException($"Could not create instance of {initializerType.FullName}.");

            initializer.Register(services, config);
            _loadedModules[modulePath] = (context, assembly);
        }

        public void UnloadModule(string modulePath)
        {
            if (_loadedModules.TryGetValue(modulePath, out var module))
            {
                _loadedModules.Remove(modulePath);
                module.Context.Unload();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }

}
