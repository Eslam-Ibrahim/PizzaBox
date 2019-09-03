using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaBox.Data.Entities
{
    public partial class PizzaBoxAppContext : DbContext
    {
        public PizzaBoxAppContext()
        {
        }

        public PizzaBoxAppContext(DbContextOptions<PizzaBoxAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crust> Crust { get; set; }
        public virtual DbSet<LocationAddress> LocationAddress { get; set; }
        public virtual DbSet<OrderTable> OrderTable { get; set; }
        public virtual DbSet<PizzaReceipe> PizzaReceipe { get; set; }
        public virtual DbSet<PizzaShopLocation> PizzaShopLocation { get; set; }
        public virtual DbSet<PizzaTable> PizzaTable { get; set; }
        public virtual DbSet<PizzasInOrder> PizzasInOrder { get; set; }
        public virtual DbSet<Size> Size { get; set; }
        public virtual DbSet<Topping> Topping { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:pizzaslice.database.windows.net,1433;Initial Catalog=PizzaBoxApp;Persist Security Info=False;User ID=localuser;Password=Password12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Crust>(entity =>
            {
                entity.ToTable("Crust", "Pizza");

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.CrustCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CrustName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LocationAddress>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK_AddressID");

                entity.ToTable("LocationAddress", "PizzaLocation");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.StateProvince)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderTable>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_OrderID");

                entity.ToTable("OrderTable", "PizzaOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrderTotalCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Location");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<PizzaReceipe>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK_PizzaID");

                entity.ToTable("PizzaReceipe", "Pizza");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.CrustId).HasColumnName("CrustID");

                entity.Property(e => e.PizzaCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PizzaDescription)
                    .IsRequired()
                    .HasMaxLength(175);

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.PizzaReceipe)
                    .HasForeignKey(d => d.CrustId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pizza_Crust");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.PizzaReceipe)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pizza_Size");
            });

            modelBuilder.Entity<PizzaShopLocation>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK_PizzaShopLocationID");

                entity.ToTable("PizzaShopLocation", "PizzaLocation");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PizzaShopLocation)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_Address");
            });

            modelBuilder.Entity<PizzaTable>(entity =>
            {
                entity.HasKey(e => e.PizzaId)
                    .HasName("PK_PizzaTable_PizzaID");

                entity.ToTable("PizzaTable", "Pizza");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.PizzaCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PizzaDescription)
                    .IsRequired()
                    .HasMaxLength(175);
            });

            modelBuilder.Entity<PizzasInOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.PizzaId })
                    .HasName("PK_OrderID_PizzaID");

                entity.ToTable("PizzasInOrder", "PizzaOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PizzasInOrder)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_ID");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzasInOrder)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaTable_ID");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size", "Pizza");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.Property(e => e.SizeCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Topping>(entity =>
            {
                entity.ToTable("Topping", "Pizza");

                entity.Property(e => e.ToppingId).HasColumnName("ToppingID");

                entity.Property(e => e.ToppingCost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ToppingName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserID");

                entity.ToTable("UserTable", "Users");

                entity.HasIndex(e => e.UserEmail)
                    .HasName("UQ__UserTabl__08638DF89DB5F310")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
