using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

public partial class SubMenu
{
    public int Id { get; set; }

    public int MenuId { get; set; }

    public string Name { get; set; } = null!;

    public int? SortOrder { get; set; }
}
