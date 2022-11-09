using System;
using System.Collections.Generic;
using Coffee.Entities;
using Coffee.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Coffee
{
    public partial class CoffeeContext : DbContext, IDbContext
    {
        public CoffeeContext()
        {
        }

        public CoffeeContext(DbContextOptions<CoffeeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Billdrink> Billdrinks { get; set; } = null!;
        public virtual DbSet<Drink> Drinks { get; set; } = null!;
        public virtual DbSet<Drinksingredient> Drinksingredients { get; set; } = null!;
        public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;database=coffee;uid=root;pwd=12345678", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bill");

                entity.Property(e => e.Id).HasMaxLength(36).HasColumnName("id");

                entity.Property(e => e.Sum).HasColumnName("sum");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Billdrink>(entity =>
            {
                entity.ToTable("billdrinks");

                entity.HasIndex(e => e.BillId, "bill_fk");

                entity.HasIndex(e => e.DrinkId, "drinkfrombill_fk");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.Coast).HasColumnName("coast");

                entity.Property(e => e.DrinkId).HasColumnName("drink_id");

                entity.HasOne(d => d.Bill)
                    .WithMany()
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("bill_fk");

                entity.HasOne(d => d.Drink)
                    .WithMany()
                    .HasForeignKey(d => d.DrinkId)
                    .HasConstraintName("drinkfrombill_fk");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("drinks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Coast).HasColumnName("coast");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.PreparingTime)
                    .HasColumnType("time")
                    .HasColumnName("preparing_time");

                entity.Property(e => e.Volume).HasColumnName("volume");
            });

            modelBuilder.Entity<Drinksingredient>(entity =>
            {

                entity.ToTable("drinksingredients");

                entity.HasIndex(e => e.DrinkId, "drink_fk");

                entity.HasIndex(e => e.IngredientsId, "ingredint_fk");

                entity.Property(e => e.DrinkId).HasColumnName("drink_id");

                entity.Property(e => e.IngredientsId).HasColumnName("ingredients_id");

                entity.HasOne(d => d.Drink)
                    .WithMany()
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("drink_fk");

                entity.HasOne(d => d.Ingredients)
                    .WithMany()
                    .HasForeignKey(d => d.IngredientsId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ingredint_fk");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "role_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .HasMaxLength(75)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(75)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("role_fk");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallet");

                entity.HasIndex(e => e.UserId, "buyer_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("buyer_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
