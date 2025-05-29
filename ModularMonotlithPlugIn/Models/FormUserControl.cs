using System;
using System.Collections.Generic;

namespace ModularMonotlithPlugIn.Models;

public partial class FormUserControl
{
    public int Id { get; set; }

    public string? UserControl { get; set; }

    public bool? IsActive { get; set; }
}
