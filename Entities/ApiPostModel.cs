using Newtonsoft.Json;

namespace TrendyolApiWithEntityFremowork.Entities
{
    public class ApiPostModel
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

        public class RootPostModel
        {
            [JsonProperty(PropertyName = "items")]
            public List<ItemPostModel> Items { get; set; }
        }

        public class ItemPostModel
        {
            [JsonProperty(PropertyName = "barcode")]
            public string Barcode { get; set; }
            public List<ImagePostModel>? images { get; set; }
            public int vatRate { get; set; }
            public string? title { get; set; }
            public string? description { get; set; }
            public string? productMainId { get; set; }
            public string? stockCode { get; set; }
            public int brandId { get; set; }
            public int categoryId { get; set; }
        }


        public class ImagePostModel
        {
            public string? url { get; set; }
        }

    }
}
