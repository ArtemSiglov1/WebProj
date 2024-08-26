using FirstProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FirstProject.BussinesLogic;

namespace FirstProject.Interfaces
{
    public interface IDataModulService
    {
        // public List<BusinessSeller> GetSellers();
        /// <summary>
        /// ищет рандомного покупателя
        /// </summary>
        /// <returns>покупатель</returns>
        public Task<Buyer> GetRandomBuyer();
        /// <summary>
        /// ищет рандомного продавца
        /// </summary>
        /// <returns>продавец</returns>
        public Task<Seller> GetRandomSeller();
        /// <summary>
        /// ищет рандомный продукт
        /// </summary>
        /// <returns>продукт</returns>
        public Product? Product(int id);
        // public Task<Magazin> AddShop();
        /// <summary>
        /// метод для инициализации бд 
        /// </summary>
        /// <param name="storage"></param>
        /// <returns>задача</returns>
        public Task InitData(IStorageService storage);
        /// <summary>
        /// метод для удаление всех данных из бд
        /// </summary>
        /// <returns></returns>
        public  Task DelleteAll();
        public Task<List<Shop>> GetShops();
        public Task<decimal> SoldOnOrders();
        public Task<decimal> ProductShipped();
        public Task<List<StatisticMagazin>> GetStatisticMagazins();


    }
}
