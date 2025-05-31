using DataContextLibr.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Server;
using System.Text.Json;
using UserContract;


namespace ModularMonotlithPlugIn.Controllers
{
    //[Route("[Controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IServiceProvider _provider;
        // private readonly ModuleLoaderDynamic _loader;
        public HomeController(IServiceProvider provider) { _provider = provider; }

        public async Task<IActionResult> Index()
        {
            var userService = _provider.GetService<IUserService>();
            if (userService == null) return Content("User module is not loaded.");
            var users = userService.GetAllUsers();
            var menu = await userService.GetMenuDtos();
            ViewBag.Menus = menu.Select(m => new SelectListItem { Value = m.MenuId.ToString(), Text = m.MenuName }).ToList();
            var viewModel = new MenuDto
            {

            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenu(MenuDto menuDto)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("FormBuilder");
            }
            var userService = _provider.GetService<IUserService>();
            if (userService == null) return Content("User module is not loaded.");
            
            var menu = await userService.createMenu(menuDto);
            if (menu.result == 0)
            {
                TempData["ErrorMessage"] = menu.errorMessage;
                return RedirectToAction("FormBuilder");
            }
            if (menu.result == 1)
                TempData["SuccessMessage"] = menu.errorMessage;
            return RedirectToAction("FormBuilder");



        }

        [HttpPost]
        public async Task<IActionResult> AddSubMenu(MenuDto menuDto)
        {

            var userService = _provider.GetService<IUserService>();
            if (userService == null) return Content("User module is not loaded.");
          
            var menu = await userService.creatSubMenu(menuDto);
            if (menu.result == 0)
            {
                TempData["ErrorMessage"] = menu.errorMessage;
                //ModelState.AddModelError("", menu.errorMessage);
                return RedirectToAction("FormBuilder");
            }
            else
            {
                TempData["SuccessMessage"] = menu.errorMessage;
                return RedirectToAction("FormBuilder");
            }


        }

        [HttpGet]
        public async Task<IActionResult> GetSubMenus(int? menuId)
        {
            var dynamic = _provider.GetService<IUserService>();
            if (dynamic == null) return Content("dynamic form module is not loaded.");
            var subMenus = await dynamic.GetSubmenu(menuId);


            return Json(subMenus);
        }


        [HttpGet]
        public async Task getMenuSubMenuDropdown()
        {
            var userService = _provider.GetService<IUserService>();
            if (userService == null) throw new Exception("Module not loaded successfully");

            var menu = await userService.GetMenuDtos();

            ViewBag.Menus = menu.Select(m => new SelectListItem { Value = m.MenuId.ToString(), Text = m.MenuName }).ToList();

            ViewBag.SubMenus = new List<SelectListItem>
            {

            };
        }

        public IActionResult ViewFrmList()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetFormDetailsJson()
        {
            var dynamicForm = _provider.GetService<IDynamicForm>();
            if (dynamicForm == null)
                return Json("Course module is not loaded.");
            var formList = await dynamicForm.getAllFormList();
            return Json(new { data = formList }); // "data" key is required by DataTables
        }

        public async Task<IActionResult> DeleteForms(int? formId)
        {
            var dynamic = _provider.GetService<IDynamicForm>();
            if (dynamic == null) return Content("dynamic form module is not loaded.");
            var deleteForm = await dynamic.DeleteForm(formId);
            return Json("Row Deleted Successs");
        }

        public async Task<IActionResult> FormBuilder(int? formId)
        {

            await getMenuSubMenuDropdown();
            var dynamic = _provider.GetService<IDynamicForm>();
            if (dynamic == null) return Content("dynamic form module is not loaded.");
            //var  DataType=await dynamic.getAllSqlDataType();
            await GetSqlDataTypeWithField();

            if (formId == null || formId == 0)
            {
                var viewModel = new DynamicFormModel
                {

                };
                return View(viewModel);
            }
            var formFields = await dynamic.getFormsWithFields(formId);
            var subMenuList = _provider.GetService<IUserService>();
            if (subMenuList == null) return Content("dynamic form module is not loaded.");
            var subMenuListData = await subMenuList.GetSubmenu(formFields.MenuDto.MenuId);
            ViewBag.subMenu = subMenuListData.Select(x => new SelectListItem
            {
                Text = x.SubMenuName,
                Value = x.SubMenuId.ToString()
            }).ToList();

            return View(formFields);
        }
        [HttpPost]
        public async Task<IActionResult> FormBuilder(string FormMode,DynamicFormModel model)
        {
            await GetSqlDataTypeWithField();
            await getMenuSubMenuDropdown();
            if (!string.IsNullOrEmpty(model.FieldsJson))
            {
                try
                {
                    model.Fields = JsonSerializer.Deserialize<List<DynamicField>>(model.FieldsJson);
                }
                catch (JsonException ex)
                {
                    ModelState.AddModelError("FieldsJson", "Invalid field data format.");
                    return View(model);
                }
            }

            if (ModelState.IsValid)
            {
                var dynamic = _provider.GetService<IDynamicForm>();
                if (dynamic == null)
                    return Content("dynamic form module is not loaded.");

                if (FormMode == "INSERT")
                {
                    var message = await dynamic.SaveFormdataAsync(model);
                    TempData["ToastType"] = message.result == 1 ? "success" : "error";
                    TempData["ToastMessage"] = message.errorMessage;
                    return View("ViewFrmList");
                }
                else if (FormMode== "UPDATE")
                {
                    var message = await dynamic.UpdateFormdataAsync(model);
                    TempData["ToastType"] = message.result == 1 ? "success" : "error";
                    TempData["ToastMessage"] = message.errorMessage;
                    return View("ViewFrmList");
                }



                return View(model);
            }


            return View(model); // Keep user inputs if model is invalid
        }

        public async Task GetSqlDataTypeWithField()
        {
            // Process the model (e.g., save to database)

            var dynamic = _provider.GetService<IDynamicForm>();
            List<SqlDataType> DataType = await dynamic.getAllSqlDataType(); // ✅ cast/assign as list

            ViewBag.sqlDataType = DataType
                .Select(x => new SelectListItem
                {
                    Value = x.Datatype?.ToString(),
                    Text = x.Datatype
                })
                .ToList();

            List<UserContract.FormUserControl> UserControl = await dynamic.getAllFormUserControl(); // ✅ cast/assign as list

            ViewBag.formUserControl = UserControl
            .Select(x => new SelectListItem
            {
                Value = x.UserControl?.ToString(),
                Text = x.UserControl
            })
            .ToList();
        }

        public async Task<IActionResult> DynamicView(int formId)
        {

            try
            {
                var dynamic = _provider.GetService<IDynamicForm>();
                if (dynamic == null) return Content("dynamic form module is not loaded.");
                var metaData = await dynamic.getFormByTableMetadata(formId);
                //var Data = await dynamic.GetOptionsAsync();



                foreach (var field in metaData.Fields)
                {
                    string fieldType = field.FieldType?.ToString() ?? "";
                    string fieldName = field.FieldName?.ToString() ?? "";
                    string optionsJson = field.OptionsJson?.ToString() ?? "";

                    if (fieldType == "DropDown" && !string.IsNullOrEmpty(field.DataSourceTable) && !string.IsNullOrEmpty(field.DataSourceValueColumn) && !string.IsNullOrEmpty(field.DataSourceTextColumn))
                    {
                        //var parts = optionsJson.Split('|');
                        var options = await dynamic.GetOptionsAsync(field.DataSourceTable, field.DataSourceValueColumn, field.DataSourceTextColumn);

                        var List = options.Select(f => new SelectListItem
                        {
                            Value = f.OptionValueField,
                            Text = f.OptionTextField
                        }).ToList();


                        ViewBag.List1 = List;
                        // Serialize list to JSON and store in TempData
                        TempData[$"Options_{fieldName}"] = Newtonsoft.Json.JsonConvert.SerializeObject(List);

                    }
                }
                return View(metaData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public async Task<IActionResult> SaveDynamicFormData(IFormCollection form)
        {
            var formData = new Dictionary<string, string>();

            foreach (var key in form.Keys)
            {
                var value = form[key].ToString();
                formData[key] = value;
            }

            var dynamic = _provider.GetService<IDynamicForm>();
            if (dynamic == null)
            {
                TempData["ToastrError"] = "Dynamic form module is not loaded.";
                return RedirectToAction("DynamicView", new { formId = 0 }); // or fallback view
            }
            int formId = int.Parse(formData["FormId"]);
            try
            {

                int metaData = dynamic.saveDynamicFormDetailRepo(formData);

                TempData["ToastrSuccess"] = "Form saved successfully.";
                return RedirectToAction("DynamicView", new { formId = formId });
            }
            catch (Exception ex)
            {
                TempData["ToastrError"] = "Error while saving form.";
                return RedirectToAction("DynamicView", new { formId = formId }); // Or handle error page
            }
        }



        [HttpGet]
        public async Task<IActionResult> getMenuSubMenuItems()
        {
            var dynamic = _provider.GetService<IUserService>();
            if (dynamic == null) return Content("dynamic form module is not loaded.");
            var subMenus = await dynamic.GetMenuSubMenuItems();

            return Json(subMenus);
        }

        [HttpPost]
        public  IActionResult DeleteField(int id)
        {
            try
            {
                var dynamic = _provider.GetService<IDynamicForm>();
                if (dynamic == null)
                {
                    TempData["ToastrError"] = "Dynamic form module is not loaded.";
                    return RedirectToAction("ViewFrmList");  
                }
                var DeleteDto = dynamic.DeleteFormFields(id);
                 
                TempData["ToastType"] = DeleteDto.result == 1 ? "success" : "error";
                TempData["ToastMessage"] = DeleteDto.errorMessage;
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFormData(string tableName)
        {
            var dynamic = _provider.GetService<IDynamicForm>();
            if (dynamic == null)
            {
                TempData["ToastrError"] = "Dynamic form module is not loaded.";
                return Json("Module Not Loaded");
                //return RedirectToAction("ViewFrmList");
            }
            var (columns, rows) = await dynamic.GetFormData(tableName);

            return Json(new
            {
                columns = columns,
                rows = rows
            });
        }
        
    }

}
