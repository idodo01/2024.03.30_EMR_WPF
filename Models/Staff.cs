using System;
using System.Collections.Generic;

namespace EMR.Models;

public partial class Staff
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string Position { get; set; } = null!;

    public string? Userimg { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Age { get; set; }
}
