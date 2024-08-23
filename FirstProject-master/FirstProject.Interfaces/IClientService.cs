using FirstProject.Data.Models;
namespace FirstProject.Interfaces
{
  public interface IClientService {
       /// <summary>
       /// создание заказов
       /// </summary>
       /// <param name="buyerId">идентиф покуп</param>
       /// <param name="sellerId">идентиф продавца</param>
       /// <param name="items">покупки</param>
       /// <returns></returns>
       public  Task<Order> CreateOrders(int buyerId, int sellerId, List<OrderItem> items);
        public Task<Order> GetOrder(int id);

    }
}