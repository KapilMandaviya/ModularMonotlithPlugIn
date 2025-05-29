//using DataContextLibr.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserContract;

namespace UserModule
{
    public class UserModuleInitializer : UserContract.IModuleInitializer
    {
        public UserModuleInitializer() { } // Make sure this exists

        public void Register(IServiceCollection services, IConfiguration config)
        {

            services.AddScoped<UserContract.IUserService, UserService>();
        }
    }

     
}

