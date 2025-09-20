using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using chat_application.Models;

namespace chat_application.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcceptedFriendship> AcceptedFriendships { get; set; }

    public virtual DbSet<BidirectionFriendship> BidirectionFriendships { get; set; }

    public virtual DbSet<FriendshipRequest> FriendshipRequests { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMessage> GroupMessages { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageRecipient> MessageRecipients { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<User> Users { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<AcceptedFriendship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AcceptedFriendship_pkey");

            entity.ToTable("AcceptedFriendship");

            entity.HasIndex(e => new { e.Userid1, e.Userid2 }, "unique_accepted_friendship_pair1").IsUnique();

            entity.HasIndex(e => new { e.Userid2, e.Userid1 }, "unique_accepted_friendship_pair2").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Userid1).HasColumnName("userid1");
            entity.Property(e => e.Userid2).HasColumnName("userid2");

            entity.HasOne(d => d.Userid1Navigation).WithMany(p => p.AcceptedFriendshipUserid1Navigations)
                .HasForeignKey(d => d.Userid1)
                .HasConstraintName("fk_acceptedfriendship_user1");

            entity.HasOne(d => d.Userid2Navigation).WithMany(p => p.AcceptedFriendshipUserid2Navigations)
                .HasForeignKey(d => d.Userid2)
                .HasConstraintName("fk_acceptedfriendship_user2");
        });

        modelBuilder.Entity<BidirectionFriendship>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("BidirectionFriendship");

            entity.Property(e => e.Friendid).HasColumnName("friendid");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<FriendshipRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FriendshipRequest");

            entity.HasIndex(e => e.Friendid, "idx_friendrequest_friend");

            entity.HasIndex(e => e.Userid, "idx_friendrequest_user");

            entity.HasIndex(e => new { e.Userid, e.Friendid }, "unique_friendship_request").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Friendid).HasColumnName("friendid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Friend).WithMany()
                .HasForeignKey(d => d.Friendid)
                .HasConstraintName("fk_friendrequest_friend");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_friendrequest_user");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Group_pkey");

            entity.ToTable("Group");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Adminid).HasColumnName("adminid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(false)
                .HasColumnName("isactive");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasOne(d => d.Admin).WithMany(p => p.Groups)
                .HasForeignKey(d => d.Adminid)
                .HasConstraintName("fk_group_admin");
        });

        modelBuilder.Entity<GroupMessage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("GroupMessage_pkey");

            entity.ToTable("GroupMessage");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("fk_groupmessage_group");

            entity.HasOne(d => d.Message).WithMany(p => p.GroupMessages)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("fk_groupmessage_message");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Message_pkey");

            entity.ToTable("Message");

            entity.HasIndex(e => e.Creatorid, "idx_message_creator");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasPrecision(6)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Creatorid).HasColumnName("creatorid");
            entity.Property(e => e.Expirydate).HasColumnName("expirydate");
            entity.Property(e => e.Isfile)
                .HasDefaultValue(false)
                .HasColumnName("isfile");
            entity.Property(e => e.Messagebody).HasColumnName("messagebody");
            entity.Property(e => e.Parentmessageid).HasColumnName("parentmessageid");

            entity.HasOne(d => d.Creator).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Creatorid)
                .HasConstraintName("fk_message_creator");
        });

        modelBuilder.Entity<MessageRecipient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MessageRecipient_pkey");

            entity.ToTable("MessageRecipient");

            entity.HasIndex(e => e.Recipientid, "idx_messagerecipient_recipient");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Isread)
                .HasDefaultValue(false)
                .HasColumnName("isread");
            entity.Property(e => e.Messageid).HasColumnName("messageid");
            entity.Property(e => e.Recipientid).HasColumnName("recipientid");

            entity.HasOne(d => d.Group).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.Groupid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_messagerecipient_group");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.Messageid)
                .HasConstraintName("fk_messagerecipient_message");

            entity.HasOne(d => d.Recipient).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.Recipientid)
                .HasConstraintName("fk_messagerecipient_user");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Profile_pkey");

            entity.ToTable("Profile");

            entity.HasIndex(e => e.Userid, "Profile_userid_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Bio)
                .HasDefaultValueSql("''::text")
                .HasColumnName("bio");
            entity.Property(e => e.Initialized)
                .HasDefaultValue(false)
                .HasColumnName("initialized");
            entity.Property(e => e.Picture)
                .HasDefaultValueSql("'https://res.cloudinary.com/dlygf7xye/image/upload/v1736098504/00721c9db2261d4ab0f9528ba9f3c7f2e70f5330.png'::text")
                .HasColumnName("picture");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.User).WithOne(p => p.Profile)
                .HasForeignKey<Profile>(d => d.Userid)
                .HasConstraintName("fk_profile_user");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "user_email_unique").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
