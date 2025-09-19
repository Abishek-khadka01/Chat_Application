using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AcceptedFriendship> AcceptedFriendshipUserid1Navigations { get; set; } = new List<AcceptedFriendship>();

    public virtual ICollection<AcceptedFriendship> AcceptedFriendshipUserid2Navigations { get; set; } = new List<AcceptedFriendship>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Profile? Profile { get; set; }
}
