namespace Basket.Api.Data.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public decimal TaotalPrice
        {
            get
            {
                //Calculate the total price of specific shopping cart
                decimal totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }

        }
    }

}
