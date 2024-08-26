using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service
{
    /// <summary>
    /// сервис клиентов 
    /// </summary>
    public class ClientService:IClientService
    {
        /// <summary>
        /// настройки для работы с бд
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;
       

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="dbContextOptions">настройки для работы с бд</param>
        public ClientService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// лист продуктов
        /// </summary>
       public List<Product>? Products { get; set; }
        /// <summary>
        /// метод для создания заказов
        /// </summary>
        /// <param name="buyerId">идентиф покупателя</param>
        /// <param name="sellerId">идентиф продавца</param>
        /// <param name="items">список покупок</param>
        /// <returns>заказ</returns>
        public async Task<Order?> CreateOrders(int buyerId,int sellerId,List<OrderItem> items)
        {
            await using var db = new DataContext(_dbContextOptions);
            var buyer = await db.Buyers.AnyAsync(x => x.Id == buyerId);
            if(!buyer)
            {
                Console.WriteLine(new BaseResponse() { IsSuccess=true,ErrorMessage="покупатель с данным не найден"});return null;
            }
            var seller=await db.Sellers.AnyAsync(db => db.Id == sellerId);
            if(!seller)
            {
                Console.WriteLine(new BaseResponse() { IsSuccess = true, ErrorMessage = "продовец с данным айди не найден" }) ;return null;
            }
            var order = new Order { BuyerId = buyerId, SellerId = sellerId, Items = items ,DateCreate=DateTime.UtcNow};
            await db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            return order;
        }
        public async Task<Order?> GetOrder(int id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var order=await db.Orders.Where(x => x.Id == id).Select(x=>new Order { Id=x.Id,
                DateCreate=x.DateCreate,
                Buyer=x.Buyer,
                Seller=x.Seller,
                BuyerId=x.BuyerId,
                Items=x.Items.Select(x=>new OrderItem()
                {
                    Id=x.Id,
                    Cost=x.Cost,
                    Count=x.Count,
                    Order=x.Order,
                    OrderId=x.OrderId,
                    Product=x.Product,
                    ProductId=x.ProductId

                }).ToList(),
                SellerId=x.SellerId,
                OrderTransaction=x.OrderTransaction
            }).FirstOrDefaultAsync();
            if(order == null)
                return null;
            return order;
        }
        
    }
}
