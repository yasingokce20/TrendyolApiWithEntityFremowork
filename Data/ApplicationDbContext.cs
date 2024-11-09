using Microsoft.EntityFrameworkCore;
using static TrendyolApiWithEntityFremowork.Entities.ApiDatabaseModel;

namespace TrendyolApiWithEntityFremowork.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        //public DbSet<RootDatabase> Roots { get; set; }
        public DbSet<ContentDatabase> Contents { get; set; }
        public DbSet<BrandDatabase> Brands { get; set; }
        public DbSet<CategoryDatabase> Categories { get; set; }
        public DbSet<ImageDatabase> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category ve Content arasında One-to-Many (Bire-Çok) ilişkiyi belirtin
            modelBuilder.Entity<ContentDatabase>()
                .HasOne(c => c.Brand)
                .WithMany(c => c.Contents)
                .HasForeignKey(c => c.BrandId);
            //.OnDelete(DeleteBehavior.Cascade); // İsteğe bağlı: Silme davranışını tanımlayın

            // Category ve Content arasında One-to-Many (Bire-Çok) ilişkiyi belirtin
            modelBuilder.Entity<ContentDatabase>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Contents)
                .HasForeignKey(c => c.CategoryId);
            //.OnDelete(DeleteBehavior.Cascade);  // İsteğe bağlı: Silme davranışını tanımlayın

            modelBuilder.Entity<ContentDatabase>()
               .HasMany(c => c.Images)                          // Content'in birden fazla Image'a sahip olduğunu belirtir
               .WithOne(i => i.Content)                         // Her Image'in bir Content'e ait olduğunu belirtir
               .HasForeignKey(i => i.Barcode);                  // Yabancı anahtarı açıkça belirtir


            // Yeni primary key olacak alanı tanımlayın
            //modelBuilder.Entity<Content>()
            //    .HasKey(b => b.IdPK);

            //modelBuilder.Entity<Brand>()
            //    .HasKey(b => b.IdPK);

            //modelBuilder.Entity<Category>()
            //    .HasKey(b => b.IdPK);

            //modelBuilder.Entity<Image>()
            //     .HasKey(b => b.IdPK);

            //modelBuilder.Entity<Root>().HasNoKey();
            //modelBuilder.Entity<Image>().HasNoKey();  // Image entity'si keyless olarak ayarlandı


        }



    }
}
