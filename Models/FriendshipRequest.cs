using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class FriendshipRequest
{
    public Guid Userid { get; set; }

    public Guid Friendid { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Friend { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
