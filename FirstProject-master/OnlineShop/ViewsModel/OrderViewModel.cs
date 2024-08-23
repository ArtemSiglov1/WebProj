using FirstProject.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.ViewsModel
{
    public class OrderViewModel
    {

        /// <summary>
        /// идентиф покупателя
        /// </summary>
        public int BuyerId { get; set; }
        /// <summary>
        /// идентиф продавца
        /// </summary>
        public int SellerId { get; set; }



      
    }

}
