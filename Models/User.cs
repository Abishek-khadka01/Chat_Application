using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Models;

[Table("users")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    public string PasswordHash { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("refresh_token")]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expires_at")]
    public DateTime? RefreshTokenExpiresAt { get; set; }

    [Column("profile_picture_url")]
    public string? ProfilePictureUrl { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<ConversationMember> ConversationMembers { get; set; } = new List<ConversationMember>();

    [InverseProperty("Receiver")]
    public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; } = new List<FriendRequest>();

    [InverseProperty("Sender")]
    public virtual ICollection<FriendRequest> FriendRequestSenders { get; set; } = new List<FriendRequest>();

    [InverseProperty("Sender")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
