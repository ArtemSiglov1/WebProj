using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    /// <summary>
    /// класс заказов
    /// </summary>
    public class Order
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// дата создания заказа
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// список заказанных предметов
        /// </summary>
        public List<OrderItem> Items { get; set; }
        /// <summary>
        /// идентиф покупателя
        /// </summary>
        public int BuyerId { get;set; }
        /// <summary>
        /// покупатель для связи 2 таблиц
        /// </summary>
        public Buyer Buyer { get; set; }
        /// <summary>
        /// идентиф продавца
        /// </summary>
        public int SellerId { get; set; }
        /// <summary>
        /// продавец
        /// </summary>
        public Seller Seller { get; set; }
        /// <summary>
        /// объект для слежения за финансами
        /// </summary>
        public List<OrderTransaction> OrderTransaction { get; set; }
    }
}
