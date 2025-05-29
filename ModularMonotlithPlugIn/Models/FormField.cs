using System;
using System.Collections.Generic;

namespace ModularMonotlithPlugIn.Models;

public partial class FormField
{
    public int Id { get; set; }

    public int FormId { get; set; }

    public string FieldName { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string DataType { get; set; } = null!;

    public string FieldType { get; set; } = null!;

    public int? LengthValue { get; set; }

    public string? DefaultValue { get; set; }

    public bool Required { get; set; }

    public bool? Duplicate { get; set; }

    public string? Tooltip { get; set; }

    public string? CssClass { get; set; }

    public int? Position { get; set; }
}
