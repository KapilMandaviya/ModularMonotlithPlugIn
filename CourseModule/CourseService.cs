using UserContract;
using DataContextLibr.Models;

using Microsoft.IdentityModel.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CourseModule
{
    public class CourseModuleInitializer : IModuleInitializer
    {
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ICourseService, CourseService>();
        }
    }
    public class CourseService : ICourseService
    {
        private readonly ModularContext _context;
        public CourseService(ModularContext context) => _context = context;

        public IEnumerable<CourseDto> GetAllCourses()
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            var getCourse = _context.CourseMasters.Select(c => new CourseDto { Id = c.Id, Title = c.Title }).ToList();
            connection.Close();
            return getCourse;
        }
    }
}
