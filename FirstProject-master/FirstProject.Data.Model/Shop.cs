
namespace FirstProject.Data.Models
{
    /// <summary>
    /// Сущность описывающая магазин
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// название магаза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// список продавцов работающих в магазе
        /// </summary>
        public List<Seller> Sellers { get; set; }
        /// <summary>
        /// список транзакций на складе привоз\отвоз
        /// </summary>
        public List<StorageTransaction> Transactions { get; set; }
    }
}