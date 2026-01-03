using Microsoft.EntityFrameworkCore;
using TeaStoreApp.Domain.Entities;

namespace TeaStoreApp.Infrastructure.Persistence;

public class TeaStoreAppDbContext(DbContextOptions<TeaStoreAppDbContext> options) : DbContext(options)
{
    // ============================
    // DbSet Mappings (Tables)
    // ============================
    public DbSet<Users> Users { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }

    /// <summary>
    /// Configures entity relationships, keys, constraints, and database-level rules using Fluent API.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ======================================================
        // 1️⃣ Users ↔ UserPhotos (One-to-One)
        // ------------------------------------------------------
        // - A user can have at most ONE profile photo
        // - A profile photo MUST belong to exactly ONE user
        // - UserPhotos is the dependent entity
        // - Deleting a user deletes their profile photo
        // ======================================================
        modelBuilder.Entity<UserPhotos>(entity =>
        {
            entity.HasKey(p => p.PhotoId);

            entity.HasOne(p => p.User)
                .WithOne(u => u.Photo)
                .HasForeignKey<UserPhotos>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforces true 1–1 relationship at DB level
            entity.HasIndex(p => p.UserId)
                .IsUnique();
        });

        // ======================================================
        // 2️⃣ Users ↔ Orders (One-to-Many)
        // ------------------------------------------------------
        // - A user can place multiple orders
        // - An order MUST belong to one user
        // - Orders is the dependent entity
        // - Deleting a user deletes their orders
        // ======================================================
        modelBuilder.Entity<Orders>(entity =>
        {
            entity.HasKey(o => o.OrderId);

            entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Monetary precision for order totals
            entity.Property(o => o.OrderTotal)
                .HasPrecision(10, 2);
        });

        // ======================================================
        // 3️⃣ Categories ↔ Products (One-to-Many)
        // ------------------------------------------------------
        // - A category can contain multiple products
        // - A product MUST belong to exactly one category
        // - Products is the dependent entity
        // - Deleting a category deletes its products
        // ======================================================
        modelBuilder.Entity<Products>(entity =>
        {
            entity.HasKey(p => p.ProductId);

            entity.Property(p => p.Price)
                .HasPrecision(10, 2);

            entity.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(p => p.Description)
                .HasMaxLength(500);

            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Improves category-based product queries
            entity.HasIndex(p => p.CategoryId);
        });

        // ======================================================
        // 4️⃣ Users ↔ ShoppingCartItems ↔ Products
        //    (Two One-to-Many Relationships)
        // ------------------------------------------------------
        // - A user can have multiple cart items
        // - A product can exist in multiple users' carts
        // - Each cart item represents ONE product for ONE user
        // - Composite uniqueness prevents duplicate cart items
        // ======================================================
        modelBuilder.Entity<ShoppingCartItems>(entity =>
        {
            entity.HasKey(c => c.CartItemId);

            entity.Property(c => c.Price)
                .HasPrecision(10, 2);

            entity.HasOne(c => c.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensures one product per user per cart
            entity.HasIndex(c => new { c.UserId, c.ProductId })
                .IsUnique();
        });

        // ======================================================
        // 5️⃣ Orders ↔ OrderItems ↔ Products
        //    (Two One-to-Many Relationships)
        // ------------------------------------------------------
        // - An order can contain multiple order items
        // - An order item belongs to exactly one order
        // - An order item represents exactly one product
        // - Product deletion is RESTRICTED to preserve history
        // ======================================================
        modelBuilder.Entity<OrderItems>(entity =>
        {
            entity.HasKey(oi => oi.OrderItemId);

            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Prevents duplicate products in the same order
            entity.HasIndex(oi => new { oi.OrderId, oi.ProductId })
                .IsUnique();

            entity.Property(oi => oi.Price)
                .HasPrecision(10, 2);

            entity.Property(oi => oi.Total)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(c => c.CategoryId);
        } );
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(p => p.UserId);
        });
    }
}