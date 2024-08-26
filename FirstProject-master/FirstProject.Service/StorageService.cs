using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.Identity.Client;
using FirstProject.Data.Enums;
using Microsoft.IdentityModel.Tokens;
using FirstProject.Interfaces;

namespace FirstProject.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class StorageService : IStorageService
    {
        /// <summary>
        /// 
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>
        public StorageService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>

        public async Task CreateShop(Shop shop)
        {
            await using var db = new DataContext(_dbContextOptions);
            if (string.IsNullOrWhiteSpace(shop.Name))
                return;

            await db.Shops.AddAsync(shop);
            await db.SaveChangesAsync();
            var test = await db.Sellers.CountAsync();


        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Shop>> GetShops()
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.ToListAsync();
            return shops;
        }
        public async Task<Shop?> GetShop(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var shops = await db.Shops.Where(x=>x.Id==shopId).Select(x=>new Shop { Id=shopId,Name=x.Name,
            Sellers=x.Sellers,Transactions=x.Transactions}).FirstOrDefaultAsync();
            if (shops == null)
            {
                return null;
            }
            return shops;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="seller"></param>
        /// <returns></returns>
        public async Task AddSeller(int shopId, Seller seller)
        {
            if (seller == null)
                return; 
            await using var db = new DataContext(_dbContextOptions);
            var shop = await db.Shops.Where(x => x.Id == shopId).FirstOrDefaultAsync();
            if (shop == null)
                return; 
            if (string.IsNullOrWhiteSpace(seller.Name))
                return; 
            if (shopId == 0)
                return; 
            seller.ShopId = shopId;
            await db.Sellers.AddAsync(seller);
            await db.SaveChangesAsync();
            return;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<Seller>> GetSellers(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var sellers = await db.Sellers.Where(x => x.ShopId == shopId).ToListAsync();
            return sellers;
        }
        public async Task<Seller?> GetSeller(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var sellers = await db.Sellers.Where(x => x.Id== id).FirstOrDefaultAsync();
            if (sellers == null)
                return null;
            return sellers;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task CreateProduct(Product product)
        {
            await using var db = new DataContext(_dbContextOptions);

            if (string.IsNullOrWhiteSpace(product.Name))
                return;
            if (product.Price < 0)
                return;

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            return;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProduct()
        {
            await using var db = new DataContext(_dbContextOptions);
            var product = await db.Products.ToListAsync();
            return product;
        }
        public async Task DeliverGoods(int shopId, int productId, double count)
        {
            await using var db = new DataContext(_dbContextOptions);
           
            var shop = await db.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
            if (shop == null)
                return; 
            var transaction = db.StorageTransactions.AddAsync(new StorageTransaction { ShopId = shopId, ProductId = productId, Count = count, TransactionType = StorageTransactionType.Take });
            await db.SaveChangesAsync();
            return;

        }
        public async Task<List<StorageTransaction>> GetStorageTransaction(int shopId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var transaction = await db.StorageTransactions.Where(x => x.ShopId == shopId).ToListAsync();
            return transaction;
        }
        public async Task GetProduct(List<OrderItem> orders, int shopId)
        {
            await using var db=new DataContext(_dbContextOptions);
            await using var transact =await db.Database.BeginTransactionAsync();
            var list = new List<StorageTransaction>();
            foreach (var item in orders)
            {
                list.Add(new StorageTransaction
                {
                    DateCreate = DateTime.UtcNow,
                    ShopId = shopId,
                    Count = Math.Truncate(item.Count * 1.5),
                    ProductId = item.ProductId,
                    TransactionType = StorageTransactionType.Take,
                });
            }
            try
            {
                await db.BulkInsertAsync(list);
                await db.SaveChangesAsync();
                await transact.CommitAsync();
            }
            catch 
            {
                await transact.RollbackAsync();
            }
        }

        public async Task AddBuyer(List<Buyer> buyers)
        {
            await using var db = new DataContext(_dbContextOptions);
            await db.BulkInsertAsync(buyers);
            await db.SaveChangesAsync();

        }
        public async Task<List<OrderTransaction>> GetOrderTransactions(int sellerId)
        {
            await using var db = new DataContext(_dbContextOptions);
            var orders=await db.Orders.Where(x=>x.SellerId==sellerId).Select(x=>x.Id).ToListAsync();
            var transactions = db.OrderTransactions.Where(x => orders.Any(y => x.OrderId == y)).ToList();
            return transactions;
        }

        //public async Task AddBuyer(List<Buyer> buyers)
        //{
        //    await using var db = new DataContext(_dbContextOptions);
        //    await db.AddRangeAsync(buyers);
        //    await db.SaveChangesAsync();

        //}
    }
}