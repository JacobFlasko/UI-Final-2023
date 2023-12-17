using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fitness_Tracker.Models
{
    public partial class ClassDatabaseContext : DbContext
    {
        public ClassDatabaseContext()
        {
        }

        public ClassDatabaseContext(DbContextOptions<ClassDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<GuifinalActivity> GuifinalActivities { get; set; } = null!;
        public virtual DbSet<GuifinalGender> GuifinalGenders { get; set; } = null!;
        public virtual DbSet<GuifinalUser> GuifinalUsers { get; set; } = null!;
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:server-for-class.database.windows.net,1433;Initial Catalog=ClassDatabase;Persist Security Info=False;User ID=MatthewWKurtz;Password=Pokemon4mwk!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("((3))");

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<GuifinalActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK__GUIFinal__393F5BA532C0FFCA");

                entity.ToTable("GUIFinal.Activity");

                entity.Property(e => e.ActivityId)
                    .ValueGeneratedNever()
                    .HasColumnName("Activity_ID");

                entity.Property(e => e.ActivityDescriptor)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Activity_Descriptor");
            });

            modelBuilder.Entity<GuifinalGender>(entity =>
            {
                entity.HasKey(e => e.GenderId)
                    .HasName("PK__GUIFinal__AF750E6481C97258");

                entity.ToTable("GUIFinal.Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Gender_ID");

                entity.Property(e => e.GenderName)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Gender_Name");
            });

            modelBuilder.Entity<GuifinalUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__GUIFinal__206D9190D3196633");

                entity.ToTable("GUIFinal.User");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("User_ID");

                entity.Property(e => e.UserActivity).HasColumnName("User_Activity");

                entity.Property(e => e.UserAge).HasColumnName("User_Age");

                entity.Property(e => e.UserBirthday)
                    .HasColumnType("date")
                    .HasColumnName("User_Birthday");

                entity.Property(e => e.UserCaloriesToLoseWeight).HasColumnName("User_CaloriesToLoseWeight");

                entity.Property(e => e.UserCurrentWeight).HasColumnName("User_CurrentWeight");

                entity.Property(e => e.UserDesiredWeight).HasColumnName("User_DesiredWeight");

                entity.Property(e => e.UserGender).HasColumnName("User_Gender");

                entity.Property(e => e.UserHeight).HasColumnName("User_Height");

                entity.Property(e => e.UserStartingWeight).HasColumnName("User_StartingWeight");

                entity.Property(e => e.UserWeightLost).HasColumnName("User_WeightLost");

                entity.HasOne(d => d.UserActivityNavigation)
                    .WithMany(p => p.GuifinalUsers)
                    .HasForeignKey(d => d.UserActivity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GUIFinal.__User___0F2D40CE");

                entity.HasOne(d => d.UserGenderNavigation)
                    .WithMany(p => p.GuifinalUsers)
                    .HasForeignKey(d => d.UserGender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GUIFinal.__User___0E391C95");
            });

            

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
