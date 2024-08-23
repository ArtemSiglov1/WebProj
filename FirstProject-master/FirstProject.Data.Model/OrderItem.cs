using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    /// <summary>
    /// информация о заказе
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// идентиф заказа
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// заказ
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// идентиф продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// стоимость 
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// количество заказанного товара
        /// </summary>
        public double Count { get; set; }

    }
}
