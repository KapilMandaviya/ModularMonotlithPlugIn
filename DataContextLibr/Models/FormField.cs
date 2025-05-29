using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

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

    public string? OptionTableName { get; set; }

    public string? OptionValueField { get; set; }

    public string? OptionTextField { get; set; }

    public string? OptionsJson { get; set; }

    public virtual Form Form { get; set; } = null!;

}
