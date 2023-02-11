namespace ShoppingCart.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //Slug is the URL name of item
        public string Slug { get; set; }

    }
}
