namespace Basket.Api.Data.Entities
{
    public class ShoppingCartItem
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; } //We Take it from Catalog Service 
        public string ProductName { get; set; }
    }
}
