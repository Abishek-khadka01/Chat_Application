using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Models;

[Table("conversations")]
public partial class Conversation
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("is_group")]
    public bool? IsGroup { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Conversation")]
    public virtual ICollection<ConversationMember> ConversationMembers { get; set; } = new List<ConversationMember>();

    [InverseProperty("Conversation")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
