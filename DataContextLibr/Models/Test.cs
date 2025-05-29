using System;
using System.Collections.Generic;

namespace DataContextLibr.Models;

public partial class Test
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int City { get; set; }

    public int State { get; set; }

    public int Pincode { get; set; }

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Photo { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Hobby { get; set; } = null!;
}
