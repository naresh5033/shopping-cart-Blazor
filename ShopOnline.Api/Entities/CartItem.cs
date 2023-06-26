namespace ShopOnline.Api.Entities
{
    // the items that has been added to the use shopping cart
    // the cart entity has one to many relationship with the cart item entity.
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }


    }
}
