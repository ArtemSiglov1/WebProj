using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.BussinesLogic
{
    /// <summary>
    /// бизнес модель магазина
    /// </summary>
    public class Magazin
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
        public IDataModulService Data { get; set; }
        public ISellerService Seller { get; set; }
        /// <summary>
        /// лист бизнес моделей продавцов
        /// </summary>
        public List<BusinessSeller> Sellers { get; set; }
        /// <summary>
        /// конструктор 
        /// </summary>
        /// <param name="dataService">интерфейс сервиса бд</param>
        /// <param name="shopId">идентиф магаза</param>
        /// <param name="sellerService">интерфейс сервиса продавца</param>
        public Magazin(IDataModulService dataService, int shopId,ISellerService sellerService)
        {
            Id = shopId;
            Data = dataService;
            Seller = sellerService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task ProcessOrder(Order order)
        {

            if (order == null) return;
            OnPassedOrders?.Invoke(this, 1);
            var response= await Seller.ProcessOrder(order, Id, order.SellerId);
            if (response)
            {
                OnThereAreOrders?.Invoke(this, 1);
            }
            else
            {
                DeliverRequest?.Invoke(this, order);
            }
        }

       

        public event EventHandler<Order> DeliverRequest;
        /// <summary>
        /// меняет кол во пришедших заказов
        /// </summary>
       public  event EventHandler<int> OnPassedOrders;
        /// <summary>
        /// меняет число выполненых заказов
        /// </summary>
        public event EventHandler<int> OnThereAreOrders;

    }
}
