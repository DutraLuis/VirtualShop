using Microsoft.EntityFrameworkCore;
using VirtualShop.ProductApi.Models;

namespace VirtualShop.ProductApi.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Category
        modelBuilder.Entity<Category>()
                    .HasKey(c => c.CategoryId);

        modelBuilder.Entity<Category>()
                    .Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired();

        //Product
        modelBuilder.Entity<Product>()
                    .Property(c => c.Name)
                    .HasMaxLength(100)
                    .IsRequired();

        modelBuilder.Entity<Product>()
                    .Property(c => c.Description)
                    .HasMaxLength(255)
                    .IsRequired();

        modelBuilder.Entity<Product>()
                    .Property(c => c.ImageURL)
                    .HasMaxLength(255)
                    .IsRequired();

        modelBuilder.Entity<Product>()
                    .Property(c => c.Price)
                    .HasPrecision(12, 2);

        modelBuilder.Entity<Category>()
                    .HasMany(c => c.Products)
                    .WithOne(c => c.Category)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Category>()
                    .HasData(
                        new Category
                        {
                            CategoryId = 1,
                            Name = "Material Escolar"
                        },
                        new Category
                        {
                            CategoryId = 2,
                            Name = "Acessórios"
                        }
                    );
    }
}