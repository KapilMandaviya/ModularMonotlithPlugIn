using System;
using System.Collections.Generic;

namespace ModularMonotlithPlugIn.Models;

public partial class Form
{
    public int Id { get; set; }

    public string? FormName { get; set; }

    public string? TableName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedId { get; set; }
}
