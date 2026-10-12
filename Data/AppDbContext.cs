using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__audit_lo__3213E83F6EB6FB96");

            entity.ToTable("audit_logs");

            entity.HasIndex(e => new { e.Action, e.CreatedAt }, "ix_audit_action").IsDescending(false, true);

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "ix_audit_entity");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "ix_audit_user_time").IsDescending(false, true);

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("entity_type");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.NewValues).HasColumnName("new_values");
            entity.Property(e => e.OldValues).HasColumnName("old_values");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500)
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UsernameInput)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username_input");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__audit_log__user___1DB06A4F");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Orders_CustomerId");

            entity.HasIndex(e => e.OrderDate, "IX_Orders_OrderDate");

            entity.HasIndex(e => e.Status, "IX_Orders_Status");

            entity.HasIndex(e => e.OrderCode, "UQ_Orders_OrderCode").IsUnique();

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Orders_CreatedAt");
            entity.Property(e => e.DeliveredAt).HasPrecision(0);
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OrderCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysdatetime())", "DF_Orders_OrderDate");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Unpaid", "DF_Orders_PaymentStatus");
            entity.Property(e => e.ReceiverName).HasMaxLength(100);
            entity.Property(e => e.ReceiverPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ShippingAddress).HasMaxLength(255);
            entity.Property(e => e.ShippingFee).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending", "DF_Orders_Status");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalAmount)
                .HasComputedColumnSql("(([Subtotal]+[ShippingFee])-[DiscountAmount])", true)
                .HasColumnType("decimal(20, 0)");
            entity.Property(e => e.UpdatedAt).HasPrecision(0);
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__permissi__3213E83FB83EEE28");

            entity.ToTable("permissions");

            entity.HasIndex(e => e.Code, "UQ__permissi__357D4CF9DA62D628").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("module");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FCCE7B684");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Code, "UQ__roles__357D4CF95C9B385D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsSystem).HasColumnName("is_system");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK__role_perm__permi__07C12930"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__role_perm__role___06CD04F7"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__role_per__C85A5463E9768F9C");
                        j.ToTable("role_permissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FABDE1C78");

            entity.ToTable("users");

            entity.HasIndex(e => new { e.BranchId, e.Status }, "ix_users_branch_status");

            entity.HasIndex(e => e.Email, "ux_users_email")
                .IsUnique()
                .HasFilter("([deleted_at] IS NULL)");

            entity.HasIndex(e => e.Username, "ux_users_username")
                .IsUnique()
                .HasFilter("([deleted_at] IS NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("avatar_url");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FailedLoginCount).HasColumnName("failed_login_count");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.LastLoginAt).HasColumnName("last_login_at");
            entity.Property(e => e.LastLoginIp)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("last_login_ip");
            entity.Property(e => e.LockedUntil).HasColumnName("locked_until");
            entity.Property(e => e.MustChangePassword)
                .HasDefaultValue(true)
                .HasColumnName("must_change_password");
            entity.Property(e => e.PasswordChangedAt).HasColumnName("password_changed_at");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ACTIVE")
                .HasColumnName("status");
            entity.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");
            entity.Property(e => e.TwoFactorSecret)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("two_factor_secret");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__users__created_b__10566F31");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK__users__updated_b__114A936A");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK__user_rol__6EDEA1537E723975");

            entity.ToTable("user_roles");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("assigned_at");
            entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__user_role__role___151B244E");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_role__user___14270015");
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user_ses__3213E83F0D58F1AD");

            entity.ToTable("user_sessions");

            entity.HasIndex(e => e.RefreshTokenHash, "UQ__user_ses__63546C908CAD10FD").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.ExpiresAt }, "ix_user_sessions_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.RefreshTokenHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("refresh_token_hash");
            entity.Property(e => e.RevokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(500)
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__user_sess__user___19DFD96B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
