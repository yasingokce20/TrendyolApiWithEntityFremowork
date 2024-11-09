using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrendyolApiWithEntityFremowork.Entities
{
    public class ApiDatabaseModel
    {

        public class ContentDatabase
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int RecordId { get; set; }

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public string? Id { get; set; }
            public int? ContentId { get; set; }
            public int SupplierId { get; set; }
            public int StoreId { get; set; }
            public string? Barcode { get; set; }
            public int BrandId { get; set; }         //   bir brand birden fazla  content nesnesi ile ilişkilendirilebilir . 
            public BrandDatabase? Brand { get; set; }    // veritabanına eklenmiyor
            public int CategoryId { get; set; }     //   bir brand birden fazla  content nesnesi ile ilişkilendirilebilir .
            public CategoryDatabase? Category { get; set; }
            public int? Quantity { get; set; }
            public double? SellingPrice { get; set; }
            public bool OnSale { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public List<ImageDatabase>? Images { get; set; }
            public string? StockCode { get; set; }

        }


        public class BrandDatabase
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int RecordId { get; set; }


            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }          // froeign key  , modelleri veritabanı nesneleri olarak ilişkilendirebilmek  için
            public string? Name { get; set; }

            [JsonIgnore] // Bu özelliği JSON serileştirmeden hariç tutar
            public ICollection<ContentDatabase>? Contents { get; set; }  // Birden fazla Content ile ilişkilendirilebilir
        }

        public class CategoryDatabase
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int RecordId { get; set; }

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }                          // froeign key  , modelleri veritabanı nesneleri olarak ilişkilendirebilmek  için
            public string? Name { get; set; }

            [JsonIgnore] // Bu özelliği JSON serileştirmeden hariç tutar
            public ICollection<ContentDatabase>? Contents { get; set; }  // Birden fazla Content ile ilişkilendirilebilir
        }


        public class ImageDatabase
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int RecordId { get; set; }
            public string? Url { get; set; }
            public string? Barcode { get; set; }   // froeign key  , modelleri veritabanı nesneleri olarak ilişkilendirebilmek  için

            [JsonIgnore] // Bu özelliği JSON serileştirmeden hariç tutar
            public ContentDatabase? Content { get; set; }
        }

    }
}







