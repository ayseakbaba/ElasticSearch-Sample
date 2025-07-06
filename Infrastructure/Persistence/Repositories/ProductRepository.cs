using Application.Dtos;
using Application.Interfaces;
using Application.Models;
using Application.Dtos;
using Infrastructure.Persistence.Context;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly MasterContext _context;

        public ProductRepository(MasterContext context)
        {
            _context = context;
        }

        public Task AddAsync(Product entity)
        {
            if (!_context.Products.Any(x => x.Id == entity.Id))
            {
                var product = new Product(
                    entity.Id,
                    entity.Name,
                    entity.Category,
                    entity.Price,
                    entity.Description,
                    entity.QuantityInStock,
                    entity.Manufacturer,
                    entity.ShippingCost,
                    entity.CustomerName,
                    entity.CustomerName,
                    entity.OrderDate,
                    entity.IsActive);
                _context.Products.Add(product);
                return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
            }
            return Task.FromResult(false);
        }

        public async Task<bool> AddRangeAsync(List<Product> entities)
        {
            await _context.Set<Product>().AddRangeAsync(entities);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }


        public Task DeleteAsync(string id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
            }
            return Task.FromResult(false);
        }

        public Task<bool> ExistsAsync(string id)
        {
            var exists = _context.Products.Any(x => x.Id == id);
            return Task.FromResult(exists); 
        }

        public Task<IEnumerable<Product>> FindAsync(Func<Product, bool> predicate)
        {
            var result = _context.Products.AsEnumerable().Where(predicate);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var productList = _context.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category,
                    Description = p.Description,    
                    QuantityInStock = p.QuantityInStock,
                    CustomerEmail = p.CustomerEmail,
                    CustomerName = p.CustomerName,
                    Manufacturer = p.Manufacturer,
                    ShippingCost = p.ShippingCost,
                    OrderDate = p.OrderDate,
                    IsActive= p.IsActive
                })
                .AsEnumerable();

            return Task.FromResult(productList);
        }

        public Task<IEnumerable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(string id)
        {
            var product = _context.Products
                .Where(x => x.Id == id)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                })
                .FirstOrDefault();

            return Task.FromResult(product);
        }


        public Task<bool> SaveChangesAsync()
        {
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public Task UpdateAsync(Product entity)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == entity.Id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Category = entity.Category;
                product.Price = entity.Price;
                product.Description = entity.Description;
                product.QuantityInStock = entity.QuantityInStock;
                product.Manufacturer = entity.Manufacturer;
                product.ShippingCost = entity.ShippingCost;
                product.CustomerName = entity.CustomerName;
                product.CustomerEmail = entity.CustomerEmail;
                product.OrderDate = entity.OrderDate;
                product.IsActive = entity.IsActive;
                return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
            }
            return Task.FromResult(false);
        }
    }
}
