using Application.Dtos;
using Application.Models;
using System.Text.Json;

namespace Business.Helpers
{
    public static class ProductHelper
    {
        public static async Task<List<Product>> LoadProductsFromJsonAsync(string filePath)
        {
            var json = await File.ReadAllTextAsync(filePath);
            var result = JsonSerializer.Deserialize<List<Product>>(json);
            return result;
        }
    }
}
