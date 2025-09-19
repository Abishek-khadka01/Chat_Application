using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class AcceptedFriendship
{
    public Guid Id { get; set; }

    public Guid Userid1 { get; set; }

    public Guid Userid2 { get; set; }

    public virtual User Userid1Navigation { get; set; } = null!;

    public virtual User Userid2Navigation { get; set; } = null!;
}
