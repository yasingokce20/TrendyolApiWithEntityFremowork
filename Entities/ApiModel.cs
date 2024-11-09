namespace TrendyolApiWithEntityFremowork.Entities
{
    public class ApiModel
    {
        public class Root
        {
            public int Page { get; set; }
            public int Size { get; set; }
            public int TotalPages { get; set; }
            public int TotalElements { get; set; }
            public List<Content>? Content { get; set; }
        }

        public class Content
        {
            public int SupplierId { get; set; }
            public int StoreId { get; set; }
            public string? Barcode { get; set; }
            public Brand? Brand { get; set; }
            public Category? Category { get; set; }
            public int? Quantity { get; set; }
            public object? OriginalPrice { get; set; }
            public double? SellingPrice { get; set; }
            public bool OnSale { get; set; }
            public string? Title { get; set; }
            public string? Id { get; set; }
            public int? ContentId { get; set; }
            public string? Description { get; set; }
            public List<Image>? Images { get; set; }
            public string? StockCode { get; set; }
            public object? RejectedReasons { get; set; }
            public object? CreatedDate { get; set; }
            public object? LastModifiedDate { get; set; }
        }


        public class Brand
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

        public class Category
        {
            public int Id { get; set; }
            public string? Name { get; set; }
        }

       
        public class Image
        {
            public string? Url { get; set; }
        }






    }


}
