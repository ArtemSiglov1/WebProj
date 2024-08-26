namespace OnlineShop.ViewsModel
{
    public class OrderTransactionViewModel
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// дата продажи товара
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// стоимость проданного товара
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// идентиф заказа
        /// </summary>
        public int OrderId { get; set; }
        
    }
}
