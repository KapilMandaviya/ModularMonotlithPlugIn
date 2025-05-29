using System;
using System.Collections.Generic;

namespace ModularMonotlithPlugIn.Models;

public partial class Shyam
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int Gender { get; set; }

    public string Mobile { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }
}
