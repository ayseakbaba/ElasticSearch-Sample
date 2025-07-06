using Application.Dtos;
using Business.Abstract;
using Business.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearch_Sample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        // Constructor added to initialize _productService
        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpPost("bulk-load")]
        public async Task<IActionResult> LoadFromJson()
        {
            var path = "C:\\Users\\aysak\\source\\repos\\ElasticSearch-Sample\\ElasticSearch-Sample\\Properties\\product mock data enable to CS class version.json";
            var products = await ProductHelper.LoadProductsFromJsonAsync(path);
            var result = await _productService.AddBulkProductsAsync(products);
            return result ? Ok("Veriler başarıyla yüklendi.") : StatusCode(500, "Hata oluştu.");
        }

        [HttpPost("add")]
        public async Task<IActionResult>AddProduct(ProductDto model)
        {
            var result = _productService.AddProductAsync(model);
            return result.Result ? Ok("Ürün başarıyla eklendi.") : StatusCode(500, "Hata oluştu.");
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _productService.SearchProductsAsync(query);
            return Ok(result);
        }
    }
}
