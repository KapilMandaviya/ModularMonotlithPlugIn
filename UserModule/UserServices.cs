
using DataContextLibr.Models;
using Microsoft.EntityFrameworkCore;
using UserContract;

namespace UserModule
{

    public class UserService : UserContract.IUserService
    {
        private readonly ModularContext _context;
        public UserService(ModularContext context)
        {
            _context = context;
        }

        public async Task<(int result, string errorMessage)> createMenu(MenuDto menuDto)
        {
            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }

            // Check for duplicate menu name
            var exists = await _context.Menus
                .AnyAsync(m => m.Name.Trim().ToLower() == menuDto.MenuName.Trim().ToLower());

            if (exists)
            {
                connection.Close();
                return (0, "Menu name already exists.");
            }

            _context.Menus.Add(new Menu { Name = menuDto.MenuName, SortOrder = menuDto.MenuSortOrder });
            int result = await _context.SaveChangesAsync();
            connection.Close(); // Dispose-like behavior
            return (result, "Menu added successfully!");
        }

        public async Task<(int result, string errorMessage)> creatSubMenu(MenuDto menu)
        {

            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }

            // Check for duplicate submenu name under same menu
            var exists = await _context.SubMenus
                .AnyAsync(sm => sm.Name.Trim().ToLower() == menu.SubMenuName.Trim().ToLower() && sm.MenuId==menu.MenuId);

            if (exists)
            {
                connection.Close();
                return (0, "SubMenu name already exists.");
            }

            _context.SubMenus.Add(new SubMenu { MenuId = menu.MenuId, Name = menu.SubMenuName, SortOrder = menu.SubMenuSortOrder });
            int result = await _context.SaveChangesAsync();
            connection.Close(); // Dispose-like behavior
            return (result, "SubMenu added successfully!");
        }

        public IEnumerable<UserContract.UserDto> GetAllUsers()
        {

            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }

            var userList = _context.UserMasters.Select(u => new UserContract.UserDto { Id = u.Id, Name = u.Name }).ToList();
            connection.Close(); // Dispose-like behavior
            return userList;
        }
        public async Task<List<MenuDto>> GetMenuDtos()
        {
            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            var getMenus = await _context.Menus.Select(u => new MenuDto
            {
                MenuId = u.Id,
                MenuName = u.Name,
                MenuSortOrder = u.SortOrder
            }).ToListAsync();
            connection.Close(); // Dispose-like behavior
            return getMenus;
        }

        public async Task<List<MenuDto>> GetMenuSubMenuItems()
        {
            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            //var result = await (from form in _context.Forms
            //                    join menu in _context.Menus on form.MenuId equals menu.Id
            //                    join submenu in _context.SubMenus on form.SubMenuId equals submenu.Id
            //                    select new
            //                    {
            //                        MenuTitle = menu.Name,
            //                        SubMenuTitle = submenu.Name,
            //                        FormId = form.Id
            //                    })
            //             .ToListAsync();
            //var result = await (from form in _context.Forms
            //                    join menu in _context.Menus on form.MenuId equals menu.Id
            //                    join submenu in _context.SubMenus on form.SubMenuId equals submenu.Id into subJoin
            //                    from sub in subJoin.DefaultIfEmpty() // Left join
            //                    select new
            //                    {
            //                        MenuTitle = menu.Name,
            //                        SubMenuTitle = sub != null ? sub.Name : null,
            //                        FormId = form.Id
            //                    })
            // .ToListAsync();

            //var grouped = result
            //    .GroupBy(x => x.MenuTitle)
            //    .Select(g => new MenuDto
            //    {
            //        MenuName = g.Key,
            //        SubMenu = g.Select(x => new UserContract.MenuDto
            //        {
            //            SubMenuName = x.SubMenuTitle,
            //            Id = x.FormId
            //        }).ToList()
            //    }).ToList();

            //return grouped;
            var result = await (from form in _context.Forms
                                join menu in _context.Menus on form.MenuId equals menu.Id
                                join submenu in _context.SubMenus on form.SubMenuId equals submenu.Id into subJoin
                                from sub in subJoin.DefaultIfEmpty()
                                select new
                                {
                                    MenuId = menu.Id,
                                    MenuTitle = menu.Name,
                                    SubMenuTitle = sub != null ? sub.Name : null,
                                    FormId = form.Id
                                })
    .ToListAsync();

            var grouped = result
                .GroupBy(x => new { x.MenuId, x.MenuTitle })
                .Select(g =>
                {
                    var subMenus = g
                        .Where(x => x.SubMenuTitle != null)
                        .Select(x => new MenuDto
                        {
                            SubMenuName = x.SubMenuTitle,
                            Id = x.FormId
                        }).ToList();

                    var menuOnlyFormId = g
                        .Where(x => x.SubMenuTitle == null)
                        .Select(x => (int)x.FormId)
                        .FirstOrDefault(); // Nullable for menus with only submenus

                    return new MenuDto
                    {
                        MenuName = g.Key.MenuTitle,
                        Id = menuOnlyFormId, // ← Only populated when no submenu
                        SubMenu = subMenus
                    };
                }).ToList();

            return grouped;


        }

        public async Task<List<Module>> GetModules()
        {
            var connection = _context.Database.GetDbConnection();


            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            var getModule = _context.Modules.Where(m => m.IsEnabled == true).ToList();
            connection.Close(); // Dispose-like behavior
            return getModule;
        }

        public async Task<List<Module>> GetModulesdisabled()
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            var getModuleDisabled = _context.Modules.Where(m => m.IsEnabled == false).ToList();
            connection.Close();
            return getModuleDisabled;
        }

        public async Task<List<MenuDto>> GetSubmenu(int? menuId)
        {
            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open(); // Dispose-like behavior
            }
            var getMenus = await _context.SubMenus.Select(u => new MenuDto
            {
                SubMenuId = u.Id,
                SubMenuName = u.Name,
                MenuSortOrder = u.SortOrder,
                MenuId = u.MenuId,
            }).Where(x => x.MenuId == menuId).ToListAsync();
            connection.Close(); // Dispose-like behavior
            return getMenus;
        }
        
    }
}
