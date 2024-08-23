using FirstProject.Data.Models;
using FirstProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.BussinesLogic
{
    /// <summary>
    /// бизнес модель продавца
    /// </summary>
    public class BusinessSeller
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// идентиф магаза
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// сервис продавца в нем описанны принципы его работы
        /// </summary>
        public SellerService Service { get; set; }
        //public List<OrderItem> ProcessOrder(Order order)
        //{

        //}
    }
}
