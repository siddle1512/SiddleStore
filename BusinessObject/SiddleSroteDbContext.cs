using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject;

public partial class SiddleSroteDbContext : DbContext
{
    public SiddleSroteDbContext()
    {
    }

    public SiddleSroteDbContext(DbContextOptions<SiddleSroteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerObject> Customers { get; set; } = null!;
    public virtual DbSet<OrderObject> Orders { get; set; } = null!;
    public virtual DbSet<OrderDetailObject> OrderDetails { get; set; } = null!;
    public virtual DbSet<ProductObject> Products { get; set; } = null!;
    public virtual DbSet<UserObject> Users { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;
    public virtual DbSet<StoreObject> Stores { get; set; } = null!;
    public virtual DbSet<DailyRevenueObject> DailyRevenues { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlData"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey("RoleId");
            entity.Property("RoleName");    
        });

        modelBuilder.Entity<Role>().HasData(
            new Role { RoleId = 1, RoleName = "Manager" },
            new Role { RoleId = 2, RoleName = "Customer" }
        );

        modelBuilder.Entity<StoreObject>(entity =>
        {
            entity.ToTable("Store");
            entity.HasKey(e => e.StoreId);
            entity.Property(e => e.StoreName);
            entity.Property(e => e.Address);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<StoreObject>().HasData(
            new StoreObject { StoreId = 1, StoreName = "Siddle01", Address = "5/30 Le Hong Phong", Status = StoreStatus.Enable },
            new StoreObject { StoreId = 2, StoreName = "Siddle02", Address = "6/30 Le Hong Phong", Status = StoreStatus.Disable }
        );

        modelBuilder.Entity<UserObject>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserId);

            entity.HasOne(o => o.Store)
                .WithMany(o => o.Users)
                .HasForeignKey(o => o.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Store");

            entity.HasOne(o => o.Role)
                .WithMany(o => o.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");

            entity.Property(e => e.UserName);
            entity.Property(e => e.PasswordHashed);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<UserObject>().HasData(
            new UserObject { UserId = 1, StoreId = 1, RoleId = 1, UserName = "Binh", PasswordHashed = "binhbo22", Status = UserStatus.Activated }
        );

        modelBuilder.Entity<DailyRevenueObject>(entity =>
        {
            entity.ToTable("DailyRevenue");
            entity.HasKey(o => new { o.Date, o.StoreId });
            entity.Property(e => e.TotalRevenue);
            entity.Property(e => e.TotalOrder);

            entity.HasOne(o => o.Store)
                .WithMany(m => m.DailyRevenues)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyRevenue_Store");
        });

        modelBuilder.Entity<CustomerObject>(entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.CustomerId);

            entity.HasOne(o => o.User)
                .WithOne(o => o.Customer)
                .HasForeignKey<CustomerObject>(e => e.UserId)
                .HasConstraintName("FK_Customer_User")
                .IsRequired(false);

            entity.Property(e => e.CustomerFullName);
            entity.Property(e => e.CustomerPhone);
            entity.Property(e => e.NationalId);
            entity.Property(e => e.Address);
            entity.Property(e => e.Balance);
        });

        modelBuilder.Entity<CustomerObject>().HasData(
            new CustomerObject { CustomerId = 1, UserId = 1, CustomerFullName = "Le Minh Vuong", CustomerPhone = "0918955649", NationalId = "054202001111", Address = "NOXH An Phu Thinh Block B", Balance = 1000000000 }
        );

        modelBuilder.Entity<OrderObject>(entity => 
        {
            entity.ToTable("Order");
            entity.HasKey(e => e.OrderId);

            entity.HasOne(o => o.Customer)
                .WithMany(m => m.Orders)
                .HasForeignKey (e => e.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer")
                .IsRequired(false);

            entity.HasOne(o => o.Store)
                .WithMany(m => m.Orders)
                .HasForeignKey(e => e.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Store");

            entity.Property(e => e.Date);
            entity.Property(e => e.Total);
            entity.Property(e => e.PaymentMethod);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<ProductObject>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey (e => e.ProductId);
            entity.Property(e => e.ProductName);
            entity.Property(e => e.Price);
            entity.Property(e => e.Discount);
            entity.Property(e => e.InStock);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<ProductObject>().HasData(
            new ProductObject { ProductId = 1, ProductName = "RTX4090", Price = 48000000, Discount = 0.2, InStock = 100, Status = ProductStatus.Available }
        );

        modelBuilder.Entity<OrderDetailObject>(entity =>
        {
            entity.ToTable("OrderDetail");
            entity.HasKey(o => new { o.OrderId, o.ProductId });

            entity.HasOne (o => o.Order)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(o => o.Product)
                .WithMany(m => m.OrderDetails)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");

            entity.Property(e => e.Quantity);
            entity.Property(e => e.Discount);
            entity.Property(e => e.SubTotal);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

