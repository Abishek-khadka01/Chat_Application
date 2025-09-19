using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class Profile
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string? Picture { get; set; }

    public bool? Initialized { get; set; }

    public string Username { get; set; } = null!;

    public string? Bio { get; set; }

    public virtual User User { get; set; } = null!;
}
