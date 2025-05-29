
using DataContextLibr.Models;
using Microsoft.EntityFrameworkCore.Storage;

//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace UserContract
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        Task<List<Module>> GetModules();
        Task<List<Module>> GetModulesdisabled();
        Task<(int result, string errorMessage)> createMenu(MenuDto menu);
        Task<(int result, string errorMessage)> creatSubMenu(MenuDto menuDto);
        Task<List<MenuDto>> GetMenuDtos();
        Task<List<MenuDto>> GetSubmenu(int? menuId);
        Task<List<MenuDto>> GetMenuSubMenuItems();



    }

    public interface IReloadModuleServices
    {
        void DynamicReloadModule();
    }

    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses();
    }
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class MenuDto {
        public int? Id { get; set; }

        //[Required(ErrorMessage = "Please select a menu.")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please select a valid menu.")]
        public int? MenuId { get; set; }
        public int? SubMenuId { get; set; }

        public string? MenuName { get; set; } 
        public string? SubMenuName { get; set; } 
        public int? MenuSortOrder { get; set; }
        public int? SubMenuSortOrder { get; set; }

        public string? SelectedMenu { get; set; }
        public string? SelectedSubMenu { get; set; }

       
        public List<MenuDto> SubMenu { get; set; } = new();
        
      
    }

    public interface IModuleInitializer
    {
        void Register(IServiceCollection services, IConfiguration config);
    }
    public interface IDynamicForm
    {
        Task<(int result, string errorMessage)> SaveFormdataAsync(DynamicFormModel form);
        Task<(int result, string errorMessage)> UpdateFormdataAsync(DynamicFormModel form);
        Task<List<SqlDataType>> getAllSqlDataType();
        Task<List<FormUserControl>> getAllFormUserControl();
        Task<DynamicFormModel> getFormByTableMetadata(int formId);
        Task<List<FormField>> GetOptionsAsync(string tableName, string valueField, string textField);
        int saveDynamicFormDetailRepo(Dictionary<string, string> keyValuePairs);

        Task<List<DynamicFormModel>> getAllFormList();

        Task<(int result, string errorMessage)> DeleteForm(int? formId);

        Task<DynamicFormModel> getFormsWithFields(int? formId);
        (int result, string errorMessage) DeleteFormFields(int? fieldId);
    }


    public class DynamicFormModel
    {

        public int Id { get; set; }
       
        [Required]
        public string FormName { get; set; }
        [Required]
        public string TableName { get; set; }
        
        public List<DynamicField> Fields { get; set; } = new List<DynamicField>();
        public string FieldsJson { get; set; } // Temporary property to capture JSON string
        public DateTime CreatedAt { get; set; }
        public int CreatedId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedId { get; set; }

        public MenuDto MenuDto{ get; set; } = new(); // 👈 Important
    }

    public class DynamicField
    {
        public int? Id { get; set; }
        [Required]
        public string FieldName { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public string DataType { get; set; }
        [Required]
        public string FieldType { get; set; }
        public string LengthValue { get; set; }
        public string DefaultValue { get; set; }
        public bool Required { get; set; }
        public bool Duplicate { get; set; }
        public string Tooltip { get; set; }
        public string CssClass { get; set; }
        public int Position { get; set; }
        public string OptionsJson { get; set; } // For static options
        public string DataSourceTable { get; set; }
        public string DataSourceValueColumn { get; set; }
        public string DataSourceTextColumn { get; set; }

        [NotMapped]
        public List<SelectListItem> DynamicOptions { get; set; } = new();
    }

     
    public class SqlDataType
    {
        public int Id { get; set; }

        public string? Datatype { get; set; }

        public bool? IsActive { get; set; }
    }

    public class FormUserControl
    {
        public int Id { get; set; }

        public string? UserControl { get; set; }

        public bool? IsActive { get; set; }
    }
}
