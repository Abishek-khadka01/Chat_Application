using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Models;

[Table("friend_requests")]
[Index("SenderId", "ReceiverId", Name = "friend_requests_sender_id_receiver_id_key", IsUnique = true)]
public partial class FriendRequest
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("sender_id")]
    public Guid? SenderId { get; set; }

    [Column("receiver_id")]
    public Guid? ReceiverId { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("sent_at", TypeName = "timestamp without time zone")]
    public DateTime? SentAt { get; set; }

    [ForeignKey("ReceiverId")]
    [InverseProperty("FriendRequestReceivers")]
    public virtual User? Receiver { get; set; }

    [ForeignKey("SenderId")]
    [InverseProperty("FriendRequestSenders")]
    public virtual User? Sender { get; set; }
}
