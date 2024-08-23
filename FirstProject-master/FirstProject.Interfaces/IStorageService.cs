using FirstProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstProject.Interfaces
{
    public interface IStorageService
    {
        /// <summary>
        /// метод для получения всех продуктов
        /// </summary>
        /// <param name="orders">заказ</param>
        /// <param name="shopId">идентиф магаза</param>
        /// <returns>рандомный товар из бд</returns>
        public Task GetProduct(List<OrderItem> orders,int shopId);
        public Task<List<Product>> GetProduct();

        /// <summary>
        /// создать магазин
        /// </summary>
        /// <param name="shop">магазин</param>
        /// <returns>задачу</returns>
        public Task CreateShop(Shop shop);

        /// <summary>
        /// выводит все магазины содержащиеся в бд
        /// </summary>
        /// <returns>лист магазинов</returns>
        public Task<List<Shop>> GetShops();
        public Task<Shop> GetShop(int shopId);

        /// <summary>
        /// добавление продавца
        /// </summary>
        /// <param name="shopId">идентиф магаза</param>
        /// <param name="seller">продавец</param>
        /// <returns>задачу</returns>
        public Task AddSeller(int shopId, Seller seller);
        /// <summary>
        /// создание продукта
        /// </summary>
        /// <param name="product">продукт</param>
        /// <returns>задача</returns>
        public Task CreateProduct(Product product);
        /// <summary>
        /// добавление покупателей
        /// </summary>
        /// <param name="buyers">лист покупателей</param>
        /// <returns>задача</returns>
        public Task AddBuyer(List<Buyer> buyers);
        /// <summary>
        /// доставка продуктов
        /// </summary>
        /// <param name="shopId">идентиф магазина</param>
        /// <param name="productId">идентиф продукта</param>
        /// <param name="count">количество</param>
        /// <returns>задача</returns>
        public Task DeliverGoods(int shopId, int productId, double count);


    }
}
