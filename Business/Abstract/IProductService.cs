using Application.Dtos;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<bool> AddProductAsync(ProductDto product);
        Task<bool> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(string id);
        Task<ProductDto> GetProductByIdAsync(string id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    }
}
