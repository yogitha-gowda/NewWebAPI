using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Models
{
    public class ProductContext : DbContext
    {
        private readonly string _conStr;

        public ProductContext()
        {
        }

        public ProductContext(string connectionString)
        {
            _conStr = connectionString;
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(_conStr))
            {
                optionsBuilder.UseSqlServer(_conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Brand
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.BrandId);
                entity.ToTable("brands", "production");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("brand_name");
            });

            // Category
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.ToTable("categories", "production");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            // Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.ToTable("products", "production");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("product_name");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ModelYear).HasColumnName("model_year");

                entity.Property(e => e.ListPrice)
                    .HasColumnType("decimal(10,2)")
                    .HasColumnName("list_price");

                // Relationships
                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId);
            });
        }
    }
}