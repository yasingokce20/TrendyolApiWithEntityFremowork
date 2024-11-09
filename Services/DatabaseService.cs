using Microsoft.EntityFrameworkCore;
using TrendyolApiWithEntityFremowork.Data;
using static TrendyolApiWithEntityFremowork.Entities.ApiDatabaseModel;
using static TrendyolApiWithEntityFremowork.Entities.ApiModel;

namespace TrendyolApiWithEntityFremowork.Services
{
    public class DatabaseService
    {
        private readonly ApplicationDbContext _dbContext;

        public DatabaseService(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        
        // GET isteği Sonrası Veritabanına ekleme ve güncelleme operasyonu , güncelleme işlemlerinin optimizasyonu henüz yapılmadı .
        public async Task SaveProductsAsync(Root products)
        {

            foreach (var content in products.Content)
            {
                // 1. Brand ekleme ya da güncelleme
                var existingBrandRecord = await _dbContext.Brands
                    .AsNoTracking() // Tracking devre dışı bırakılır
                    .FirstOrDefaultAsync(b => b.Id == content.Brand.Id );

                if (existingBrandRecord == null)
                {
                    existingBrandRecord = new BrandDatabase
                    {
                        Id = content.Brand.Id,
                        Name = content.Brand.Name
                    };
                    _dbContext.Brands.Add(existingBrandRecord);
                }
                else
                {
                    // Eğer güncelleme yapılacaksa, entity'yi izlemeye al.
                    if (existingBrandRecord.Name != content.Brand.Name)
                    {
                        _dbContext.Brands.Attach(existingBrandRecord);  // Attach ile izlemeye alınıyor
                        existingBrandRecord.Name = content.Brand.Name;  // Değişiklik yapılıyor
                    }
                }

                // 2.  Category ekleme ya da güncelleme
                var existingCategoryRecord = await _dbContext.Categories
                    .AsNoTracking() // Tracking devre dışı bırakılır
                    .FirstOrDefaultAsync(c => c.Id == content.Category.Id);

                if (existingCategoryRecord == null)
                {
                    existingCategoryRecord = new CategoryDatabase
                    {
                        Id = content.Category.Id,
                        Name = content.Category.Name
                    };
                    _dbContext.Categories.Add(existingCategoryRecord);
                }
                else
                {
                    // Eğer güncelleme yapılacaksa, entity'yi izlemeye al.
                    if (existingCategoryRecord.Name != content.Category.Name)
                    {
                        _dbContext.Categories.Attach(existingCategoryRecord);
                        existingCategoryRecord.Name = content.Category.Name;  // Değişiklik yapılıyor
                    }
                }

                // 3. Content ekleme ya da güncelleme
                var exsitingContentRecord = await _dbContext.Contents
                    .AsNoTracking() // Tracking devre dışı bırakılır
                    .FirstOrDefaultAsync(c => c.Id == content.Id);

                if (exsitingContentRecord == null)
                {
                    exsitingContentRecord = new ContentDatabase
                    {
                        Id=content.Id,
                        ContentId = content.ContentId,
                        SupplierId = content.SupplierId,
                        StoreId = content.StoreId,
                        Barcode = content.Barcode,
                        BrandId = existingBrandRecord.Id, // Foreign key
                        CategoryId = existingCategoryRecord.Id, // Foreign key
                        Quantity = content.Quantity,
                        SellingPrice = content.SellingPrice,
                        OnSale = content.OnSale,
                        Title = content.Title,
                        Description = content.Description,
                        StockCode = content.StockCode,
                        Images = content.Images.Select(i => new ImageDatabase
                        {
                            Url = i.Url,
                            Barcode = content.Barcode
                        }).ToList()
                    };
                    _dbContext.Contents.Add(exsitingContentRecord);
                }
                else
                {
                    // Eğer güncelleme yapılacaksa izlemeye al ve değişiklikleri yap , daha optimize bir güncelleme işlemi daha sonra yapılacak.
                    if (exsitingContentRecord.Barcode != content.Barcode)
                    {
                        _dbContext.Contents.Attach(exsitingContentRecord); // Attach ile izlemeye alınıyor
                        exsitingContentRecord.SupplierId = content.SupplierId;
                        exsitingContentRecord.StoreId = content.StoreId;
                        exsitingContentRecord.Barcode = content.Barcode;
                        exsitingContentRecord.BrandId = existingBrandRecord.Id;
                        exsitingContentRecord.CategoryId = existingCategoryRecord.Id;
                        exsitingContentRecord.Quantity = content.Quantity;
                        exsitingContentRecord.SellingPrice = content.SellingPrice;
                        exsitingContentRecord.OnSale = content.OnSale;
                        exsitingContentRecord.Title = content.Title;
                        exsitingContentRecord.Description = content.Description;
                        exsitingContentRecord.StockCode = content.StockCode;

                        // Image güncelleme ya da ekleme işlemi
                        exsitingContentRecord.Images = content.Images.Select(i => new ImageDatabase
                        {
                            Url = i.Url,
                            Barcode = content.Barcode
                        }).ToList();
                    }
                }

                // Değişiklikleri veritabanına kaydet
                await _dbContext.SaveChangesAsync();

            }

        }

    }

    
}
