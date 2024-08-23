using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Data.Models
{
    /// <summary>
    /// класс покупателя
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// идентиф покуп
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список заказов
        /// </summary>
        public List<Order> Orders { get; set;}
    }
}
