using Lab7Layout.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab7Layout.Data;

public class Lab7LayoutContext(DbContextOptions<Lab7LayoutContext> options) : DbContext(options)
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasIndex(category => category.Name)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasOne(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

    public static async Task SeedAsync(Lab7LayoutContext database)
    {
        if (await database.Categories.AnyAsync())
        {
            return;
        }

        var category = new Category { Name = "Featured", Status = 1 };
        database.Categories.Add(category);
        database.Products.AddRange(
            new Product
            {
                Name = "Everyday tote",
                Price = 42,
                SalePrice = 35,
                Status = 1,
                Category = category,
                Image = "https://images.unsplash.com/photo-1544816155-12df9643f363?w=900",
                Description = "A durable everyday carryall with a clean, timeless profile."
            },
            new Product
            {
                Name = "Ceramic table vase",
                Price = 58,
                SalePrice = 0,
                Status = 1,
                Category = category,
                Image = "https://images.unsplash.com/photo-1578500494198-246f612d3b3d?w=900",
                Description = "A hand-finished ceramic vase for fresh stems or a simple shelf accent."
            },
            new Product
            {
                Name = "Soft-knit throw",
                Price = 76,
                SalePrice = 64,
                Status = 1,
                Category = category,
                Image = "https://images.unsplash.com/photo-1600210492486-724fe5c67fb0?w=900",
                Description = "A relaxed layer in a soft neutral weave, made for slow mornings."
            });
        await database.SaveChangesAsync();
    }
}
