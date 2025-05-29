//using DataContextLibr.Models;
using DataContextLibr.Models;
using Microsoft.EntityFrameworkCore;

using UserContract;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ModularContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptions => sqlServerOptions.CommandTimeout(300)));


builder.Services.AddSingleton<ModuleLoader>();



// Build service provider temporarily to resolve DbContext and load modules
var tempProvider = builder.Services.BuildServiceProvider();
using (var scope = tempProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ModularContext>();
    var config = builder.Configuration;
    var loader = new ModuleLoader();

    var enabledModules = db.Modules.Where(m => m.IsEnabled == true).ToList();
    var connection = db.Database.GetDbConnection();
    foreach (var module in enabledModules)
    {
        loader.LoadModule(module.DllPath, builder.Services, config);
    }

    if (connection.State != System.Data.ConnectionState.Closed)
    {
        connection.Close(); // Dispose-like behavior
    }



    //Re - register module loader with loaded modules
    builder.Services.AddSingleton(loader);
}

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
