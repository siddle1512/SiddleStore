using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject;

public partial class SiddleStoreDbContext : DbContext
{
    public SiddleStoreDbContext()
    {
    }

    public SiddleStoreDbContext(DbContextOptions<SiddleStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerObject> Customers { get; set; } = null!;
    public virtual DbSet<OrderObject> Orders { get; set; } = null!;
    public virtual DbSet<OrderDetailObject> OrderDetails { get; set; } = null!;
    public virtual DbSet<ProductObject> Products { get; set; } = null!;
    public virtual DbSet<UserObject> Users { get; set; } = null!;
    public virtual DbSet<RoleObject> Roles { get; set; } = null!;
    public virtual DbSet<StoreObject> Stores { get; set; } = null!;
    public virtual DbSet<DailyRevenueObject> DailyRevenues { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlData"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleObject>(entity =>
        {
            entity.HasKey("RoleId");
            entity.Property("RoleName");    
        });

        modelBuilder.Entity<RoleObject>().HasData(
            new RoleObject { RoleId = 1, RoleName = "Manager" },
            new RoleObject { RoleId = 2, RoleName = "Customer" },
            new RoleObject { RoleId = 3, RoleName = "Employee" }
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
            new UserObject { UserId = 1, StoreId = 1, RoleId = 1, UserName = "Binh", PasswordHashed = "0JmNw88xNIslS+BAKIG3KgBSDPW4NKibp2qKL3/bunA=", Status = UserStatus.Activated }
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
            new CustomerObject { CustomerId = 1, UserId = null, CustomerFullName = "Hua Duc Binh", CustomerPhone = "0918955649", NationalId = "054202001111", Address = "NOXH An Phu Thinh Block B", Balance = 1000000000 }
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
            entity.Property(e => e.Total)
                  .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentMethod);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<ProductObject>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey (e => e.ProductId);
            entity.Property(e => e.ProductName);
            entity.Property(e => e.Price)
                  .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Discount);
            entity.Property(e => e.InStock);
            entity.Property(e => e.Status);
        });

        modelBuilder.Entity<ProductObject>().HasData(
            new ProductObject { ProductId = 1, ProductName = "RTX4090", Price = 48000000, Discount = 0.1, InStock = 100, Status = ProductStatus.Available },
            new ProductObject { ProductId = 2, ProductName = "RTX4080", Price = 24000000, Discount = 0.1, InStock = 100, Status = ProductStatus.Available },
            new ProductObject { ProductId = 3, ProductName = "RTX4070", Price = 17000000, Discount = 0.1, InStock = 100, Status = ProductStatus.Available },
            new ProductObject { ProductId = 4, ProductName = "RTX4060", Price = 14000000, Discount = 0.1, InStock = 100, Status = ProductStatus.Available }
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
            entity.Property(e => e.SubTotal)
                  .HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

