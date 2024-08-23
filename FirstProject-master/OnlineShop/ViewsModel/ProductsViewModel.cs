namespace OnlineShop.ViewsModel
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }   
        public double Count {  get; set; }
        public double Price {  get; set; }
        public decimal Cost { get =>(decimal) (Price * Count); }
    }
}
