using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

public partial class SqlDatatypeList
{
    public int Id { get; set; }

    public string? Datatype { get; set; }

    public bool? IsActive { get; set; }
}
