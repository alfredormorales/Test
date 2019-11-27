using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Test.EFModels
{
    public partial class testDBContext : DbContext
    {
        public testDBContext()
        {
        }

        public testDBContext(DbContextOptions<testDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Receipt> Receipt { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserModule> UserModule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=testDB.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Receipt>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasDefaultValueSql("1");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Receipt)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Receipt)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .IsUnique();

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Status).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<UserModule>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.UserId, e.ModuleId });

                entity.Property(e => e.Status).HasDefaultValueSql("1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserModule)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
