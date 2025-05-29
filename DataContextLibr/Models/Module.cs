using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

public partial class Module
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? DllPath { get; set; }

    public bool? IsEnabled { get; set; }
}
