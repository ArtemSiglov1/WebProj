using FirstProject.Data.Enums;

namespace OnlineShop.ViewsModel
{
    public class StorageTransactionViewModel
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Count { get; set; }
        public DateTime DateCreate { get; set; }
        public StorageTransactionType TransactionType { get; set; }
    }
}
