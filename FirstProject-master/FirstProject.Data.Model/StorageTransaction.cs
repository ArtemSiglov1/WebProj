using FirstProject.Data.Enums;
namespace FirstProject.Data.Models
{
    /// <summary>
    /// транзации на складе магазина
    /// </summary>
    public class StorageTransaction
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// идентиф магазина
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// магазин
        /// </summary>
        public Shop Shop { get; set; }
        /// <summary>
        /// идентиф продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Product Products { get; set; }
        /// <summary>
        /// количество
        /// </summary>
        public double Count { get; set; }
        /// <summary>
        /// дата транзакции
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// тип транзакции привоз\отвоз
        /// </summary>
        public StorageTransactionType TransactionType { get; set; }
    }
}
