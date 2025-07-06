using Application.Dtos;
using Application.Models;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductDto product);
        Task<bool> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(string id);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<bool> AddBulkProductsAsync(List<Product> products);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string query);

    }
}
