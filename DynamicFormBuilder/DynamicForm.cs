﻿
using DataContextLibr.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using System.Runtime.CompilerServices;
using UserContract;

namespace DynamicFormBuilder
{
    public class DynamicFormModuleInitializer : UserContract.IModuleInitializer
    {
        public DynamicFormModuleInitializer()
        {

        }
        public void Register(IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<UserContract.IDynamicForm, DynamicForm>();
        }
    }
    public class DynamicForm : UserContract.IDynamicForm
    {
        private readonly ModularContext _context;

        public DynamicForm(ModularContext context) => _context = context;

        public async Task<(int result, string errorMessage)> SaveFormdataAsync(UserContract.DynamicFormModel form)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open(); // Dispose-like behavior
                }
                bool tableExists = _context.Forms.Any(x => x.TableName.ToLower() == form.TableName.ToLower());
                bool FormMenuSubMenueExists = _context.Forms.Any(x => x.MenuId == form.MenuDto.MenuId && x.SubMenuId == form.MenuDto.SubMenuId && x.FormName == form.FormName);
                bool MenuSubMenuExists = _context.Forms.Any(x => x.MenuId == form.MenuDto.MenuId && x.SubMenuId == form.MenuDto.SubMenuId);
                bool formNameExists = _context.Forms.Any(x => x.FormName.ToLower() == form.FormName.ToLower());

                if (MenuSubMenuExists)
                {
                    return (0, "This menu and submenu combination already has a form assigned");
                }
                if (formNameExists)
                {
                    return (0, "Form name already exists.");
                }

                if (FormMenuSubMenueExists)
                {
                    return (0, "A form is already assigned to menu and submenu.");
                }



                var formdata = new Form
                {
                    FormName = form.FormName,
                    TableName = form.TableName,
                    MenuId = form.MenuDto.MenuId,
                    SubMenuId = form.MenuDto.SubMenuId,

                    CreatedAt = DateTime.Now,
                    CreatedId = form.CreatedId,
                    UpdatedAt = null,
                    UpdatedId = form.UpdatedId
                };

                this._context.Forms.Add(formdata);
                this._context.SaveChanges();

                //formdata.FormId is now available (auto - incremented value)
                Console.WriteLine("Generated Form ID: " + formdata.Id); // Debug this value

                if (formdata.Id == 0)
                {
                    throw new Exception("FormId was not generated. Check model/database.");
                }

                //2.Map and assign FormId to FormFields
                List<FormField> formFields = form.Fields.Select(dto => new FormField
                {
                    FormId = formdata.Id, // ✅ Assign the FK here
                    FieldName = dto.FieldName,
                    CssClass = dto.CssClass,
                    DataType = dto.DataType,
                    DefaultValue = dto.DefaultValue,
                    Duplicate = dto.Duplicate,
                    FieldType = dto.FieldType,
                    Label = dto.Label,
                    LengthValue = string.IsNullOrWhiteSpace(dto.LengthValue) ? (int?)null : int.Parse(dto.LengthValue),
                    Position = dto.Position,
                    Required = dto.Required,
                    Tooltip = dto.Tooltip,
                    OptionsJson = dto.OptionsJson,
                    OptionTableName = dto.DataSourceTable,
                    OptionTextField = dto.DataSourceTextColumn,
                    OptionValueField = dto.DataSourceValueColumn

                }).ToList();

                // 3.Save form fields
                this._context.FormFields.AddRange(formFields);
                this._context.SaveChanges();


                var fieldsSql = form.Fields.Select(f =>
                {
                    string type = f.DataType.ToUpper();
                    string length = (type == "VARCHAR" || type == "NVARCHAR") && int.Parse(f.LengthValue) != null ? $"({f.LengthValue})" : "";
                    string nullable = f.Required ? "NOT NULL" : "NULL";
                    return $"{f.FieldName} {type}{length} {nullable}";
                });

                var sql = "";

                if (tableExists == false)
                {
                    sql = $"CREATE TABLE {form.TableName} (Id INT PRIMARY KEY IDENTITY(1,1), {string.Join(", ", fieldsSql)})";
                }
                else
                {

                    var alterStatements = form.Fields.Select(f =>
                    {
                        string type = f.DataType.ToUpper();
                        string length = (type == "VARCHAR" || type == "NVARCHAR") ? $"({f.LengthValue})" : "";
                        string nullable = "NULL";
                        return $"ALTER TABLE {form.TableName} ADD {f.FieldName} {type}{length} {nullable};";
                    });

                    sql = string.Join(" ", alterStatements); // multiple ALTERs
                }

                using var transaction = _context.Database.BeginTransaction();
                try
                {


                    this._context.Database.ExecuteSqlRaw(sql);

                    transaction.Commit();
                    connection.Close();
                    return (1, "Form and fields saved successfully.");
                }
                catch (Exception ws)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw ws.InnerException;
                }
            }
            catch (Exception ws)
            {

                throw;
            }
        }



        public async Task<List<UserContract.SqlDataType>> getAllSqlDataType()
        {
            try
            {
                var sqlDatatypeList = await _context.SqlDatatypeLists.Select(x => new UserContract.SqlDataType
                {
                    Datatype = x.Datatype,
                    Id = x.Id,
                    IsActive = x.IsActive
                }).ToListAsync();
                return sqlDatatypeList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<UserContract.FormUserControl>> getAllFormUserControl()
        {
            try
            {
                var formUserControls = await _context.FormUserControls.Select(x => new UserContract.FormUserControl
                {
                    UserControl = x.UserControl,
                    Id = x.Id,
                    IsActive = x.IsActive
                }).ToListAsync();
                return formUserControls;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<UserContract.DynamicFormModel> getFormByTableMetadata(int formId)
        {
            try
            {
                // int formId =3006;

                var formWithFields = _context.Forms
                    .Where(f => f.Id == formId)
                    .Select(f => new DynamicFormModel
                    {
                        Id = f.Id,
                        FormName = f.FormName ?? "",
                        TableName = f.TableName ?? "",


                        Fields = f.Fields.Select(field => new DynamicField
                        {
                            Id = field.Id,
                            FieldName = field.FieldName,
                            CssClass = field.CssClass ?? "",
                            DataType = field.DataType,
                            DefaultValue = field.DefaultValue ?? "",
                            Duplicate = field.Duplicate ?? false,
                            FieldType = field.FieldType,
                            Label = field.Label,
                            LengthValue = field.LengthValue.ToString() ?? "",
                            Position = field.Position ?? 0,
                            Required = field.Required,
                            Tooltip = field.Tooltip ?? "",
                            OptionsJson = field.OptionsJson ?? "",
                            DataSourceTable = field.OptionTableName ?? "",
                            DataSourceTextColumn = field.OptionTextField ?? "",
                            DataSourceValueColumn = field.OptionValueField ?? ""

                        }).ToList()
                    })
                    .FirstOrDefault();
                return formWithFields;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<FormField>> GetOptionsAsync(string tableName, string valueField, string textField)
        {
            var options = new List<FormField>();
            var connectionString = _context.Database.GetDbConnection().ConnectionString;
            //  _logger.LogInformation($"ConnectionString: {connectionString ?? "NULL"}");

            using (var context = new ModularContext(new DbContextOptionsBuilder<ModularContext>()
                .UseSqlServer(connectionString)
                .Options))
            {
                var sql = $"SELECT [{valueField}] AS Value, [{textField}] AS Text FROM [{tableName}]";
                using (var conn = context.Database.GetDbConnection())
                {
                    // _logger.LogInformation($"New context connection state: {conn.State}, ConnectionString: {conn.ConnectionString ?? "NULL"}");
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        await conn.OpenAsync();
                    }

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                options.Add(new FormField
                                {
                                    OptionValueField = reader["Value"].ToString(),
                                    OptionTextField = reader["Text"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return options;
        }

        public int saveDynamicFormDetailRepo(Dictionary<string, string> keyValuePairs)
        {

            if (!keyValuePairs.TryGetValue("TableName", out string tableName) || string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentException("TableName is missing in the form data.");

            // Remove TableName so only field data remains
            keyValuePairs.Remove("TableName");
            keyValuePairs.Remove("FormName");
            keyValuePairs.Remove("FormId");

            if (keyValuePairs.Count == 0)
                throw new ArgumentException("No form fields to save.");

            // Build SQL
            var columnNames = string.Join(", ", keyValuePairs.Keys.Select(k => $"[{k}]"));
            var parameterNames = string.Join(", ", keyValuePairs.Keys.Select(k => $"@{k}"));
            var sql = $"INSERT INTO [{tableName}] ({columnNames}) VALUES ({parameterNames})";
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            // Execute using ADO.NET
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                foreach (var kvp in keyValuePairs)
                {
                    cmd.Parameters.AddWithValue("@" + kvp.Key, kvp.Value ?? (object)DBNull.Value);
                }

                conn.OpenAsync();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }

        }

        public async Task<List<DynamicFormModel>> getAllFormList()
        {
            //var formsList = await _context.Forms
            //    .Include(x => x.Men)
            //    .Include(x => x.SubMenu)
            //    .Select(x=> new DynamicFormModel
            //{
            //    FormName = x.FormName ?? "",
            //    TableName = x.TableName ?? "",
            //    Id = x.Id,
            //    MenuId = x.MenuId,
            //    SubMenuId = x.SubMenuId
            //}).ToListAsync();

            var formsList = await (from form in _context.Forms
                                   join menu in _context.Menus on form.MenuId equals menu.Id
                                   join subMenu in _context.SubMenus on form.SubMenuId equals subMenu.Id
                                   select new DynamicFormModel
                                   {
                                       Id = form.Id,
                                       FormName = form.FormName ?? "",
                                       TableName = form.TableName ?? "",
                                       MenuDto = new MenuDto
                                       {
                                           MenuId = form.MenuId,
                                           SubMenuId = form.SubMenuId,
                                           MenuName = menu.Name,
                                           SubMenuName = subMenu.Name
                                       },


                                   }).ToListAsync();

            return formsList;

        }

        public async Task<DynamicFormModel> getFormsWithFields(int? formId)
        {
            var formWithFields = await _context.Forms
                    .Where(f => f.Id == formId)
                    .Select(f => new DynamicFormModel
                    {
                        Id = f.Id,
                        FormName = f.FormName ?? "",
                        TableName = f.TableName ?? "",
                        MenuDto = new MenuDto
                        {
                            MenuId = f.MenuId ?? 0,
                            SubMenuId = f.SubMenuId ?? 0,
                        },

                        Fields = f.Fields.Select(field => new DynamicField
                        {
                            Id = field.Id,
                            FieldName = field.FieldName,
                            CssClass = field.CssClass ?? "",
                            DataType = field.DataType,
                            DefaultValue = field.DefaultValue ?? "",
                            Duplicate = field.Duplicate ?? false,
                            FieldType = field.FieldType,
                            Label = field.Label,
                            LengthValue = field.LengthValue.ToString() ?? "",
                            Position = field.Position ?? 0,
                            Required = field.Required,
                            Tooltip = field.Tooltip ?? "",
                            OptionsJson = field.OptionsJson ?? "",
                            DataSourceTable = field.OptionTableName ?? "",
                            DataSourceTextColumn = field.OptionTextField ?? "",
                            DataSourceValueColumn = field.OptionValueField ?? ""

                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

            return formWithFields;
        }

        public async Task<(int result, string errorMessage)> DeleteForm(int? formId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var form = await _context.Forms.FirstOrDefaultAsync(x => x.Id == formId);
                if (form == null)
                    return (0, "Form not found");

                _context.Forms.Remove(form);
                await _context.SaveChangesAsync();

                // Delete all related form fields
                var formFields = await _context.FormFields.Where(x => x.FormId == formId).ToListAsync();
                if (formFields.Any())
                {
                    _context.FormFields.RemoveRange(formFields);
                    await _context.SaveChangesAsync();
                }
                using (var context = new ModularContext())
                {
                    string sql = $"DROP TABLE [{form.TableName}]";
                    context.Database.ExecuteSqlRaw(sql);
                }

                await transaction.CommitAsync();

                return (1, "Form deleted successfully");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (0, $"Error occurred: {ex.Message}");
            }
        }

        public async Task<(int result, string errorMessage)> UpdateFormdataAsync(DynamicFormModel form)
        {
            try
            {
                var connection = _context.Database.GetDbConnection();

                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open(); // Dispose-like behavior
                }


                var FormExists = await _context.Forms.FirstOrDefaultAsync(x => x.FormName.ToLower().ToString() == form.FormName.ToLower() && x.TableName.ToLower() != form.TableName.ToLower());
                var menuExists = await _context.Forms.FirstOrDefaultAsync(x => x.MenuId == form.MenuDto.MenuId && x.TableName.ToLower() != form.TableName.ToLower());
                var subMenuExists = await _context.Forms.FirstOrDefaultAsync(x => x.SubMenuId == form.MenuDto.SubMenuId && x.TableName.ToLower() != form.TableName.ToLower());
                if (FormExists != null)
                {
                    return (0, "This form name already used");
                }
                if (menuExists != null || subMenuExists != null)
                {
                    return (0, "This Menu and Submenu already used with different Form");
                }

                // 
                var tableExists = await _context.Forms.FirstOrDefaultAsync(x => x.TableName.ToLower() == form.TableName.ToLower());
                if (tableExists != null)
                {
                    tableExists.FormName = form.FormName;
                    tableExists.TableName = form.TableName;
                    tableExists.MenuId = form.MenuDto.MenuId;
                    tableExists.SubMenuId = form.MenuDto.SubMenuId;
                    tableExists.UpdatedAt = DateTime.Now;
                    tableExists.UpdatedId = form.UpdatedId;
                }
                this._context.SaveChanges();


                //formdata.FormId is now available (auto - incremented value)
                Console.WriteLine("Generated Form ID: " + tableExists.Id); // Debug this value

                if (tableExists.Id == 0)
                {
                    throw new Exception("FormId was not generated. Check model/database.");
                }

                // 1. Get existing form fields for the form
                var existingFields = await _context.FormFields
                    .Where(f => f.FormId == tableExists.Id)
                    .ToListAsync();
                var sql = "";
                // 2. Loop through incoming fields (DTOs)
                foreach (var dto in form.Fields)
                {
                    // Try to find an existing field (match by Id, or define your logic)
                    var existingField = existingFields.FirstOrDefault(f => f.Id == dto.Id);

                    if (existingField != null)
                    {
                        // Update existing field
                        existingField.FieldName = dto.FieldName;
                        existingField.CssClass = dto.CssClass;
                        existingField.DataType = dto.DataType;
                        existingField.DefaultValue = dto.DefaultValue;
                        existingField.Duplicate = dto.Duplicate;
                        existingField.FieldType = dto.FieldType;
                        existingField.Label = dto.Label;
                        existingField.LengthValue = string.IsNullOrWhiteSpace(dto.LengthValue) ? (int?)null : int.Parse(dto.LengthValue);
                        existingField.Position = dto.Position;
                        existingField.Required = dto.Required;
                        existingField.Tooltip = dto.Tooltip;
                        existingField.OptionsJson = dto.OptionsJson;
                        existingField.OptionTableName = dto.DataSourceTable;
                        existingField.OptionTextField = dto.DataSourceTextColumn;
                        existingField.OptionValueField = dto.DataSourceValueColumn;
                    }
                    else
                    {
                        // New field — add it
                        var newField = new FormField
                        {
                            FormId = tableExists.Id,
                            FieldName = dto.FieldName,
                            CssClass = dto.CssClass,
                            DataType = dto.DataType,
                            DefaultValue = dto.DefaultValue,
                            Duplicate = dto.Duplicate,
                            FieldType = dto.FieldType,
                            Label = dto.Label,
                            LengthValue = string.IsNullOrWhiteSpace(dto.LengthValue) ? (int?)null : int.Parse(dto.LengthValue),
                            Position = dto.Position,
                            Required = dto.Required,
                            Tooltip = dto.Tooltip,
                            OptionsJson = dto.OptionsJson,
                            OptionTableName = dto.DataSourceTable,
                            OptionTextField = dto.DataSourceTextColumn,
                            OptionValueField = dto.DataSourceValueColumn
                        };

                        await _context.FormFields.AddAsync(newField);

                        // 1. Get existing column names from the table
                        var existingColumns = await _context.Database
                            .SqlQueryRaw<string>($@" SELECT COLUMN_NAME 
                                            FROM INFORMATION_SCHEMA.COLUMNS 
                                            WHERE TABLE_NAME = '{form.TableName}'
                                        ").ToListAsync();

                        // Normalize to match case-insensitive
                        var existingColumnSet = new HashSet<string>(existingColumns, StringComparer.OrdinalIgnoreCase);

                        // 2. Generate ALTER TABLE statements only for new fields
                        var alterStatements = form.Fields
                            .Where(f => !existingColumnSet.Contains(f.FieldName)) // Only new fields
                            .Select(f =>
                            {
                                string type = f.DataType.ToUpper();
                                string length = (type == "VARCHAR" || type == "NVARCHAR") ? $"({f.LengthValue})" : "";
                                //string nullable = f.Required ? "NOT NULL" : "NULL";
                                string nullable = "NULL";
                                return $"ALTER TABLE {form.TableName} ADD {f.FieldName} {type}{length} {nullable};";
                            });

                        // 3. Join into one SQL string (optional)
                        sql = string.Join(" ", alterStatements); // multiple ALTERs
                    }
                }
                // 3. Save changes
                await _context.SaveChangesAsync();
                 
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    if(!string.IsNullOrEmpty(sql))
                        this._context.Database.ExecuteSqlRaw(sql);

                    transaction.Commit();
                    connection.Close();
                    return (1, "Form and fields Updated successfully.");
                }
                catch (Exception ws)
                {
                    transaction.Rollback();
                    connection.Close();
                    throw ws.InnerException;
                }
            }
            catch (Exception ws)
            {

                throw;
            }
        }

        public  (int result, string errorMessage) DeleteFormFields(int? fieldId)

        {
            using var transaction =  _context.Database.BeginTransaction();

            try
            {
                var formField =  _context.FormFields.FirstOrDefault(x => x.Id == fieldId);
                if (formField == null)
                    return (0, "Field not found");

                _context.FormFields.Remove(formField);
                 _context.SaveChanges();

                var TableName = _context.Forms.FirstOrDefault(x => x.Id == formField.FormId);
                // Delete all related form fields
               
                using (var context = new ModularContext())
                {
                    string sql = $"ALTER TABLE {TableName.TableName } DROP COLUMN {formField.FieldName}";
                    context.Database.ExecuteSqlRaw(sql);
                }

                 transaction.Commit();

                return (1, "Field deleted successfully");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return (0, $"Error occurred: {ex.Message}");
            }
        }
    }

}
