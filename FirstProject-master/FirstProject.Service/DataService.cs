using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    /// <summary>
    /// сервис для работы с бд
    /// </summary>
    public class DataService : IDataModulService
    {
        /// <summary>
        /// настройки для работы с бд
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="dbContextOptions">настройки для работы с бд</param>

        public DataService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// найти рандомного покупателя
        /// </summary>
        /// <returns>покупатель</returns>
        public async Task<Buyer> GetRandomBuyer()
        {
            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var buyers = await db.Buyers.ToListAsync();
            var buyer = buyers[random.Next(0, buyers.Count-1)];
            return buyer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Product Product(int id)
        {
            using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var product = db.Products.Where(x=>x.Id==id).Select(x=>new Product {Name=x.Name,Price=x.Price }).FirstOrDefault();
            if (product == null) return null; 
            return product;
        }
        public async Task<List<Product>> GetProducts(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var products = await db.Products.Where(x=>x.Id==id).ToListAsync(); 
            return products;
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task DelleteAll()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.ToListAsync();
            await db.Shops.BulkDeleteAsync(shops);
            var seller = await db.Sellers.ToListAsync();
            await db.Sellers.BulkDeleteAsync(seller);
            var store = await db.StorageTransactions.ToListAsync();
            await db.StorageTransactions.BulkDeleteAsync(store);
            var buyer = await db.Buyers.ToListAsync();
            await db.Buyers.BulkDeleteAsync(buyer);
            var order = await db.Orders.ToListAsync();
            await db.Orders.BulkDeleteAsync(order);
            var orderItem = await db.OrderItems.ToListAsync();
            await db.OrderItems.BulkDeleteAsync(orderItem);
            var orderTransact = await db.OrderTransactions.ToListAsync();
            await db.OrderTransactions.BulkDeleteAsync(orderTransact);
            var product = await db.Products.ToListAsync();
            await db.Products.BulkDeleteAsync(product);
            await db.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Seller> GetRandomSeller()
        {

            await using var db = new DataContext(_dbContextOptions);
            Random random = new Random();
            var sellers = await db.Sellers.ToListAsync();
            var seller = sellers[random.Next(0, sellers.Count - 1)];
            return seller;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public async Task InitData(IStorageService storage)
        {
            Random random = new Random();
            for(int i = 0; i < 100; i++)
            {
                await storage.CreateProduct(new Product() { Name = Guid.NewGuid().ToString(), Price = random.Next(1, 1000) });
            }
            List<Buyer> buyerList = new List<Buyer>();
            for(int j=0; j<10; j++)
            {
                buyerList.Add(new Buyer {Name = Guid.NewGuid().ToString(),});
            }
            await storage.AddBuyer(buyerList);

            for (int i = 0; i < 10; i++)
            {
                List<Seller> sellers = new List<Seller>();
                List<StorageTransaction> transactions = new List<StorageTransaction>();
                var shops = await storage.GetShops();
                


               for (int j = 0; j < 8; j++)
                {
                    sellers.Add(new Seller() { Name = Guid.NewGuid().ToString() });
                }
                for (int j = 0; j < 50; j++)
                {

                    //transactions.Add(new StorageTransaction() { ProductId =(await GetRandomProduct()).Id, Count = 500,TransactionType=Data.Enums.StorageTransactionType.Take});
                }
                await storage.CreateShop(new Shop() { Name = Guid.NewGuid().ToString(), Sellers = sellers, Transactions = transactions});

               
            }
        }

        public async Task<List<Shop>> GetShop()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shop=await db.Shops.Select(x=>new Shop { Id=x.Id,Name=x.Name,Sellers=x.Sellers}).ToListAsync();
           
            return shop;
        }

        public async Task<decimal> SoldOnOrders()
        {
            await using var db = new DataContext(_dbContextOptions);
            var soldOnOrders = (decimal)await db.Orders
                .SelectMany(order => order.Items)
                .SumAsync(item => item.Count);

            return soldOnOrders;
        }

        public async Task<decimal> ProductShipped()
        {
            await using var db = new DataContext(_dbContextOptions);
            var productShipped =(decimal) await db.Shops
                .SelectMany(shop => shop.Transactions)
                .Where(transaction => transaction.TransactionType == Data.Enums.StorageTransactionType.Ship)
                .SumAsync(transaction => transaction.Count);

            return productShipped;
        }

        public async Task<List<StatisticMagazin>> GetStatisticMagazins()
        {
            await using var db = new DataContext(_dbContextOptions);
            var statistic=db.StorageTransactions
                .Where(y => y.TransactionType == Data.Enums.StorageTransactionType.Ship&&y.Shop!=null)
                .Select(u => new StatisticMagazin
                {
                    ShopName = u.Shop.Name,
                    SellsTovar = u.Count,
                    Cost = (decimal)(u.Count * u.Products.Price)

                }).ToList();
            return statistic;
        }


    }
}
