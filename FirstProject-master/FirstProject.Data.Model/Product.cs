using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    /// <summary>
    /// продукт
    /// </summary>
    public class Product
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// цена продукта
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// список транзакций продукта привоз\отвоз
        /// </summary>
        public List<StorageTransaction> Transactions { get; set; }
        /// <summary>
        /// список покупок в заказе
        /// </summary>
        public List<OrderItem> Items { get; set; }
    }
}
