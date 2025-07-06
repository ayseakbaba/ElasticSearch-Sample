using Domain.Models;

namespace Application.Models
{
    public class Product : BaseEntity
    {
        public  string Name { get; set; }
        public  string Category { get; set; }
        public float Price { get; set; }
        public  string Description { get; set; }
        public int QuantityInStock { get; set; }
        public  string Manufacturer { get; set; }
        public float ShippingCost { get; set; }
        public  string CustomerName { get; set; }
        public  string CustomerEmail { get; set; }
        public  string OrderDate { get; set; }
        public bool IsActive { get; set; }

        public Product()
        {
            
        }

        public Product(string name, string category, string category1, float price, string description, int quantityInStock, string manufacturer, float shippingCost, string customerName, string customerEmail, string orderDate, bool isActive)
        {
            Name = name;
            Category = category;
            Price = price;
            Description = description;
            QuantityInStock = quantityInStock;
            Manufacturer = manufacturer;
            ShippingCost = shippingCost;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            OrderDate = orderDate;
            IsActive = isActive;
        }
    }
}
