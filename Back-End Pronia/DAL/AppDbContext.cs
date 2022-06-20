using Back_End_Pronia.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End_Pronia.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PlantCategory> PlantCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
