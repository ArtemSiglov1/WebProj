namespace OnlineShop.ViewsModel
{
    public class ShopCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SellerViewModel> Sellers { get; set; }
    }
}
