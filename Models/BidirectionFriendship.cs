using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class BidirectionFriendship
{
    public Guid? Userid { get; set; }

    public Guid? Friendid { get; set; }
}
