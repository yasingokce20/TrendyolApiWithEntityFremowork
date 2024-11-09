using Microsoft.AspNetCore.Mvc;
using TrendyolApiWithEntityFremowork.Services;
using static TrendyolApiWithEntityFremowork.Entities.ApiModel;
using static TrendyolApiWithEntityFremowork.Entities.ApiPostModel;

namespace TrendyolApiWithEntityFremowork.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ApiModelController : ControllerBase
    {

        private readonly TrendyolApiService _trendyolApiService;

        public ApiModelController(TrendyolApiService trendyolApiService)
        {
            _trendyolApiService = trendyolApiService;
        }


        [HttpGet]
        [Route("/[controller]/[action]")]      // şunu kaldır ve hatayı gör.
        public async Task<ActionResult<Root>> GetAllProduct()
        {
            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/stores/404/products"; // Kendi URL'nizi kullanın
            var products = await _trendyolApiService.GetProductsAsync(apiUrl);
            return Ok(products);
        }


        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<Content>> GetProductByBarcode(string barcode)
        {
            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/stores/404/products";
            var product = await _trendyolApiService.GetProductByBarcodeAsync(apiUrl, barcode);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound($"Barcode ile ilişkili ürün bulunamadı: {barcode}");
        }


        [HttpGet("brandId/{BrandId}")]
        public async Task<ActionResult<Content>> GetProductByBrand(int BrandId)
        {
            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/stores/404/products";
            var product = await _trendyolApiService.GetProductsByBrandAsync(apiUrl, BrandId);

            if (product != null)
            {
                return Ok(product);
            }

            return NotFound($"Brand ile ilişkili ürün bulunamadı: {BrandId}");
        }


        [HttpPost]
        [Route("/[controller]/AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] RootPostModel newProduct)
        {

            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/products"; // Kendi API URL'nizi kullanın

            try
            {
                var result = await _trendyolApiService.AddProductAsync(apiUrl, newProduct);

                if (result)
                {
                    return Ok("Ürün başarıyla eklendi.");
                }
                else
                {
                    return BadRequest("Ürün eklenemedi.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }



        [HttpPut]
        [Route("/[controller]/UpdateProduct/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Content updatedProduct)
        {
            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/products"; // Kendi API URL'nizi kullanın

            try
            {
                var result = await _trendyolApiService.UpdateProductAsync(apiUrl, updatedProduct);

                if (result)
                {
                    return Ok("Ürün başarıyla güncellendi.");
                }
                else
                {
                    return BadRequest("Ürün güncellenemedi.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("/[controller]/DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var apiUrl = "https://stageapi.trendyol.com/grocerygw/suppliers/249371/stores/404/products"; // Kendi API URL'nizi kullanın

            try
            {
                var result = await _trendyolApiService.DeleteProductAsync(apiUrl, id);

                if (result)
                {
                    return Ok("Ürün başarıyla silindi.");
                }
                else
                {
                    return BadRequest("Ürün silinemedi.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }

        }



    }


}



