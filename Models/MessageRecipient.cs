using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class MessageRecipient
{
    public Guid Id { get; set; }

    public Guid Recipientid { get; set; }

    public Guid? Groupid { get; set; }

    public bool? Isread { get; set; }

    public Guid Messageid { get; set; }

    public virtual Group? Group { get; set; }

    public virtual Message Message { get; set; } = null!;

    public virtual User Recipient { get; set; } = null!;
}
