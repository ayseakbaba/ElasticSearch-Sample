namespace Application.Dtos
{
    public record ProductDto
    {
        public  string Id { get; set; }
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

    }
}
