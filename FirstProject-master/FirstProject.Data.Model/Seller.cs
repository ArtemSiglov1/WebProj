using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    /// <summary>
    /// продавец
    /// </summary>
    public class Seller
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя продавца
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// идентиф магазина
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// магазин
        /// </summary>
        public Shop Shop { get; set; }
        /// <summary>
        /// список заказов которые получил этот продавец
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
