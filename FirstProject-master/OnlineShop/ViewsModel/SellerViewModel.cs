namespace OnlineShop.ViewsModel
{
    public class SellerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShopId {  get; set; }
        List<RequestViewModel> requests { get; set; }
    }
}
