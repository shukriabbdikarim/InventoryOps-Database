using Microsoft.EntityFrameworkCore;
using InventoryOps.ConsoleApp.Models;

namespace InventoryOps.ConsoleApp.Data;

public class InventoryOpsContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderRow> OrderRows => Set<OrderRow>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();

    private readonly string _connectionString;

    public InventoryOpsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Customers
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.CustomerId);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Suppliers
        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Suppliers");
            entity.HasKey(e => e.SupplierId);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ContactEmail).HasMaxLength(200).IsRequired();
            entity.HasIndex(e => e.ContactEmail).IsUnique();
        });

        // Products
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
            entity.HasOne(e => e.Supplier)
                  .WithMany(s => s.Products)
                  .HasForeignKey(e => e.SupplierId);
        });

        // Orders
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(e => e.OrderId);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("GETDATE()");
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Orders)
                  .HasForeignKey(e => e.CustomerId);
        });

        // OrderRows
        modelBuilder.Entity<OrderRow>(entity =>
        {
            entity.ToTable("OrderRows");
            entity.HasKey(e => e.OrderRowId);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
            entity.HasOne(e => e.Order)
                  .WithMany(o => o.OrderRows)
                  .HasForeignKey(e => e.OrderId);
            entity.HasOne(e => e.Product)
                  .WithMany(p => p.OrderRows)
                  .HasForeignKey(e => e.ProductId);
        });

        // StockMovements
        modelBuilder.Entity<StockMovement>(entity =>
        {
            entity.ToTable("StockMovements");
            entity.HasKey(e => e.MovementId);
            entity.Property(e => e.Reason).HasMaxLength(100).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            entity.HasOne(e => e.Product)
                  .WithMany(p => p.StockMovements)
                  .HasForeignKey(e => e.ProductId);
        });
    }
}
