using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace p7_C00299690_C00299553
{
    public partial class p7_C00299690_C00299553 : DbContext
    {
        public p7_C00299690_C00299553()
        {
        }

        public p7_C00299690_C00299553(DbContextOptions<p7_C00299690_C00299553> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; } = null!;
        public virtual DbSet<Order> Order { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItem { get; set; } = null!;
        public virtual DbSet<Product> Product { get; set; } = null!;
        public virtual DbSet<Supplier> Supplier { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("Filename=C:\\Users\\jonat\\RiderProjects\\p7_C00299690_C00299553\\smallbusiness.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TotalAmount).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Quantity).HasDefaultValueSql("1");

                entity.Property(e => e.UnitPrice).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.IsDiscontinued).HasDefaultValueSql("0");

                entity.Property(e => e.UnitPrice).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
