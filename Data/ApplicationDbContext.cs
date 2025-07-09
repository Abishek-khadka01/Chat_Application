using System;
using System.Collections.Generic;
using Chat_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat_Application.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationMember> ConversationMembers { get; set; }

    public virtual DbSet<FriendRequest> FriendRequests { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=chat_application;Username=postgres;Password=@Qqa124");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conversations_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsGroup).HasDefaultValue(false);
        });

        modelBuilder.Entity<ConversationMember>(entity =>
        {
            entity.HasKey(e => new { e.ConversationId, e.UserId }).HasName("conversation_members_pkey");

            entity.Property(e => e.JoinedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Conversation).WithMany(p => p.ConversationMembers).HasConstraintName("conversation_members_conversation_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ConversationMembers).HasConstraintName("conversation_members_user_id_fkey");
        });

        modelBuilder.Entity<FriendRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("friend_requests_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.SentAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'::character varying");

            entity.HasOne(d => d.Receiver).WithMany(p => p.FriendRequestReceivers)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("friend_requests_receiver_id_fkey");

            entity.HasOne(d => d.Sender).WithMany(p => p.FriendRequestSenders)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("friend_requests_sender_id_fkey");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("messages_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.SentAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Conversation).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("messages_conversation_id_fkey");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("messages_sender_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
