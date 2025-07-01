using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IINSwalayan.Models;

namespace IINSwalayan.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Banner> Banners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Product configuration
            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)");
            });

            // Order configuration
            builder.Entity<Order>(entity =>
            {
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(18,2)");
            });

            // OrderItem configuration
            builder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18,2)");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18,2)");
            });

            // CartItem configuration
            builder.Entity<CartItem>(entity =>
            {
                entity.Property(e => e.UnitPrice)
                    .HasColumnType("decimal(18,2)");
            });

            // Relationships
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data for Categories
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Makanan", Description = "Berbagai jenis makanan", IconClass = "fas fa-utensils", IsActive = true },
                new Category { Id = 2, Name = "Minuman", Description = "Berbagai jenis minuman", IconClass = "fas fa-coffee", IsActive = true },
                new Category { Id = 3, Name = "Kebutuhan Dapur", Description = "Peralatan dan bahan dapur", IconClass = "fas fa-kitchen-set", IsActive = true },
                new Category { Id = 4, Name = "Skincare", Description = "Produk perawatan kulit", IconClass = "fas fa-spray-can", IsActive = true }
            );

            // Seed data for Products
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Indomie Goreng",
                    Description = "Mie instan rasa ayam bawang",
                    Price = 3500,
                    Stock = 100,
                    CategoryId = 1,
                    ImageUrl = "/images/products/indomie.jpg",
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Aqua 600ml",
                    Description = "Air mineral dalam kemasan",
                    Price = 3000,
                    Stock = 200,
                    CategoryId = 2,
                    ImageUrl = "/images/products/aqua.jpg",
                    IsActive = true
                }
            );

            // Banner configuration
            builder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(e => e.LinkUrl).HasMaxLength(500);
                entity.Property(e => e.ButtonText).HasMaxLength(50);
                entity.Property(e => e.BackgroundColor).HasMaxLength(7);
                entity.Property(e => e.TextColor).HasMaxLength(7);
                entity.HasIndex(e => e.Order);
                entity.HasIndex(e => e.IsActive);
                entity.HasIndex(e => e.StartDate);
                entity.HasIndex(e => e.EndDate);
            });

            // Seed data for Banners
            builder.Entity<Banner>().HasData(
                new Banner
                {
                    Id = 1,
                    Title = "MIDMONTH MADNESS",
                    Description = "Harga Spesial untuk Semua GIFTCARD - Periode 17-23 Juni 2025",
                    ImageUrl = "https://via.placeholder.com/800x300/FF69B4/FFFFFF?text=MIDMONTH+MADNESS",
                    LinkUrl = "/Home/Products",
                    ButtonText = "KLIK DI SINI",
                    BackgroundColor = "#FF69B4",
                    TextColor = "#FFFFFF",
                    Order = 1,
                    IsActive = true,
                    StartDate = new DateTime(2025, 6, 17),
                    EndDate = new DateTime(2025, 6, 23),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Banner
                {
                    Id = 2,
                    Title = "INDONESIA JUARA",
                    Description = "Exclusive Collectible Figurine - Hanya 10 STAMP + Rp 29.900",
                    ImageUrl = "https://via.placeholder.com/800x300/1E3A8A/FFFFFF?text=INDONESIA+JUARA",
                    LinkUrl = "/Home/Products",
                    ButtonText = "KLIK DI SINI",
                    BackgroundColor = "#1E3A8A",
                    TextColor = "#FFFFFF",
                    Order = 2,
                    IsActive = true,
                    StartDate = new DateTime(2025, 6, 1),
                    EndDate = new DateTime(2025, 6, 30),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );
        }
    }
}
