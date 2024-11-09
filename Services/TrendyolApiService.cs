using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static TrendyolApiWithEntityFremowork.Entities.ApiModel;
using static TrendyolApiWithEntityFremowork.Entities.ApiPostModel;

namespace TrendyolApiWithEntityFremowork.Services
{
    public class TrendyolApiService
    {

        private readonly HttpClient _httpClient;
        private readonly string? _authorizationHeader;
        private readonly string? _cookieHeader;
        private readonly DatabaseService _databaseService; // DatabaseService örneği


        public TrendyolApiService(HttpClient httpClient, IConfiguration configuration, DatabaseService databaseService)
        {
            _httpClient = httpClient;
            _authorizationHeader = configuration["TrendyolApi:AuthorizationHeader"];   // appsettings.json'dan değerleri alıyoruz
            _cookieHeader = configuration["TrendyolApi:CookieHeader"];
            _databaseService = databaseService;
        }


        public async Task<Root> GetProductsAsync(string apiUrl)
        {

            // Authorization başlığını ekle
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);

            // Cookie başlığını ekle
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);


            // HTTP GET isteği gönder
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);


            // İstek başarılıysa, sonucu deserialize et ve döndür
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                Root products = JsonConvert.DeserializeObject<Root>(jsonResponse);


                //  Veritabanına kaydetme işlemi
                await SaveProductsToDatabase(products);

                return products;
            }
            else
            {
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }

        }




        public async Task<Content> GetProductByBarcodeAsync(string apiUrl, string barcode)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<Root>(jsonResponse);

                // Barcode'e göre ürünü filtrele
                var product = products.Content?.FirstOrDefault(p => p.Barcode == barcode);

                return product;
            }
            else
            {
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }





        public async Task<List<Content>> GetProductsByBrandAsync(string apiUrl, int brandId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<Root>(jsonResponse);

                // Marka adına göre filtreleme yap
                var filteredProducts = products.Content.Where(p => p.Brand.Id == brandId).ToList();          // filteredProducts türü Content listesi. 

                return filteredProducts;
            }
            else
            {
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }



        public async Task<bool> AddProductAsync(string apiUrl, RootPostModel newProduct)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);

            var jsonProduct = JsonConvert.SerializeObject(newProduct);
            var item = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, item);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();  // Hata mesajını inceleyin
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }




        public async Task<bool> UpdateProductAsync(string apiUrl, Content updatedProduct)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);

            var jsonProduct = JsonConvert.SerializeObject(updatedProduct);
            var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }




        public async Task<bool> DeleteProductAsync(string apiUrl, int productId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _authorizationHeader);
            _httpClient.DefaultRequestHeaders.Add("Cookie", _cookieHeader);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"{apiUrl}/{productId}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new Exception($"API isteği başarısız: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }



        private async Task SaveProductsToDatabase(Root products)
        {
            // Veritabanına kaydetme işlemi
            await _databaseService.SaveProductsAsync(products); // Örnek bir metot çağrısı
        }



    }



}













