using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.DB_Context;

public partial class UsersContext : DbContext
{
    public UsersContext()
    {
    }

    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAcc> UserAccs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=postgres4api.hopto.org;Port=5432;Database=Users;Username=postgres;Password=penek2281337");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("name");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("states_pkey");

            entity.ToTable("states");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user");
        });

        modelBuilder.Entity<UserAcc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_acc_pkey");

            entity.ToTable("user_acc");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Login)
                .HasMaxLength(30)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .HasColumnName("password");
            entity.Property(e => e.UserGroupId).HasColumnName("user_group_id");
            entity.Property(e => e.UserStateId).HasColumnName("user_state_id");

            entity.HasOne(d => d.UserGroup).WithMany(p => p.UserAccs)
                .HasForeignKey(d => d.UserGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_acc_user_group_id_fkey");

            entity.HasOne(d => d.UserState).WithMany(p => p.UserAccs)
                .HasForeignKey(d => d.UserStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_acc_user_state_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
