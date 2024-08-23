using FirstProject.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewsModel
{
    public class RequestViewModel
    {
        public int BuyerId { get; set; }
        /// <summary>
        /// идентиф продавца
        /// </summary>
        public int SellerId { get; set; }
        public List<ProductsViewModel> Products { get; set; } = null;
    }
}
