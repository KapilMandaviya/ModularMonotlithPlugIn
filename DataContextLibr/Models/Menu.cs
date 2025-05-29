using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? SortOrder { get; set; }
}
