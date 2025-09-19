using System;
using System.Collections.Generic;

namespace chat_application.Models;

public partial class GroupMessage
{
    public Guid Id { get; set; }

    public Guid GroupId { get; set; }

    public Guid MessageId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual Message Message { get; set; } = null!;
}
