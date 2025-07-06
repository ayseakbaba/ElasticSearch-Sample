using Application.Dtos;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Business.Abstract;
namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IElasticRepository<ProductDto> _elasticRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IElasticRepository<ProductDto> elasticRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _elasticRepository = elasticRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> AddProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            return await _elasticRepository.IndexAsync(productDto);
        }

        public async Task<bool> UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(product);
            return await _elasticRepository.UpdateAsync(productDto); // aynı id ile overwrite eder
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            await _productRepository.DeleteAsync(id);
            return await _elasticRepository.DeleteAsync(id);
        }

        public async Task<bool> AddBulkProductsAsync(List<Product> products)
        {
            return await _productRepository.AddRangeAsync(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string query)
        {
            return await _elasticRepository.SearchAsync(query);
        }
    }
}
