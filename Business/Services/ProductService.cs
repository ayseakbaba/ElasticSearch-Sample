using Business.Abstract;
using Application.Dtos;
namespace Business.Services
{
    internal class ProductService : IProductService
    {
        public Task<bool> AddProductAsync(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
