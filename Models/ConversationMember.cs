using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Models;

[PrimaryKey("ConversationId", "UserId")]
[Table("conversation_members")]
public partial class ConversationMember
{
    [Key]
    [Column("conversation_id")]
    public Guid ConversationId { get; set; }

    [Key]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("joined_at", TypeName = "timestamp without time zone")]
    public DateTime? JoinedAt { get; set; }

    [ForeignKey("ConversationId")]
    [InverseProperty("ConversationMembers")]
    public virtual Conversation Conversation { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ConversationMembers")]
    public virtual User User { get; set; } = null!;
}
