using System.Reflection;
using System.Runtime.Loader;

namespace UserContract {
    using System.Reflection;
    using System.Runtime.Loader;

    namespace UserContract
    {
        public class PluginLoadContext : AssemblyLoadContext
        {
            private readonly AssemblyDependencyResolver _resolver;

            public PluginLoadContext(string pluginPath) : base(true)
            {
                _resolver = new AssemblyDependencyResolver(pluginPath);
            }

            protected override Assembly? Load(AssemblyName assemblyName)
            {
                var path = _resolver.ResolveAssemblyToPath(assemblyName);
                return path != null ? LoadFromAssemblyPath(path) : null;
            }
            
            
        }
    }
}
