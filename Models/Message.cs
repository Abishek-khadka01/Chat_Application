using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class Message
{
    public Guid Id { get; set; }

    public string Messagebody { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public Guid? Parentmessageid { get; set; }

    public DateOnly? Expirydate { get; set; }

    public Guid Creatorid { get; set; }

    public bool? Isfile { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();
}
