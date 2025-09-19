using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class Group
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? Createdat { get; set; }

    public bool? Isactive { get; set; }

    public Guid Adminid { get; set; }

    public virtual User Admin { get; set; } = null!;

    public virtual ICollection<GroupMessage> GroupMessages { get; set; } = new List<GroupMessage>();

    public virtual ICollection<MessageRecipient> MessageRecipients { get; set; } = new List<MessageRecipient>();
}
