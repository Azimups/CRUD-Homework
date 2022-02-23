using Fiorella.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.DataLayerAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }

        public DbSet<About> AboutImg { get; set; }

        public DbSet<AboutText> AboutTexts { get; set; }

        public DbSet<Expert> Experts { get; set; }

        public DbSet<ExpertTitle> ExpertTitles { get; set; }

        public DbSet<BlogTitle> BlogTitles { get; set; }

        public DbSet<Blogger> Bloggers { get; set; }

        public  DbSet<saySlider> SaySliders { get; set; }

        public DbSet<SubscribeImg> SubscribeImg { get; set; }
        
        public DbSet<InstagramImg> InstagramImg { get; set; }

        public DbSet<Bio> Bios { get; set; }

        public DbSet<BioHeader> BioHeaders { get; set; }


    }
}