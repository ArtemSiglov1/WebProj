using FirstProject.Data.Models;
using FirstProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FirstProject.BussinesLogic
{
    /// <summary>
    /// бизнес модель клиент
    /// </summary>
    public class Client
    {
        /// <summary>
        /// идентиф покупателя
        /// </summary>
        public int BuyerId { get; set; }
        /// <summary>
        /// конструктор 
        /// </summary>
        /// <param name="clientService">сервис клиента</param>
        /// <param name="dataModul">модель бд и работы с ней</param>
        public Client(IClientService clientService, IDataModulService dataModul)
        {
            ClientService = clientService;
            DataModulService= dataModul;
        }
        /// <summary>
        /// интерфейс сервиса клиента
        /// </summary>
        public IClientService ClientService { get; set; }
        /// <summary>
        /// интерфейс сервиса бд
        /// </summary>
        public IDataModulService DataModulService { get; set; }
        /// <summary>
        /// таймер для событий
        /// </summary>
        private System.Timers.Timer _timer;
        /// <summary>
        /// метод для запуска таймера
        /// </summary>
        /// <param name="intervalInMilliseconds">интервал таймер ждет</param>

        //public void StartTimer(double intervalInMilliseconds)
        //{
        //    _timer = new System.Timers.Timer(intervalInMilliseconds);

        //    // Подписка на событие Elapsed, которое срабатывает при истечении интервала
        //    _timer.Elapsed += OnTimedEvent;

        //    // Автоматический перезапуск таймера
        //    _timer.AutoReset = true;
        //    _timer.Start();


        //}
        /// <summary>
        /// метод для получения случайного списка покупок
        /// </summary>
        /// <returns>список покупок</returns>
        //private async Task<List<OrderItem>> GenerateRandomOrder()
        //{
        //    List<OrderItem> orderItems = new List<OrderItem>();
        //    Random random = new Random();
        //    int n = random.Next(1, 25);
        //    for (int i = 0; i < n; i++)
        //    {
        //        var product =await DataModulService.GetRandomProduct();
        //        var randomCount=random.Next(1, 100);
        //            var cost = product.Price * randomCount;

        //        orderItems.Add(new OrderItem() {ProductId = product.Id, Count = randomCount,Cost = (decimal)cost });
        //    }
        //    return orderItems;
        //}
        /// <summary>
        /// метод который выполняется по истечению таймера
        /// </summary>
        /// <param name="source">?</param>
        /// <param name="e">?</param>
        //private async void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    var buyer =await DataModulService.GetRandomBuyer();
        //    var seller =await DataModulService.GetRandomSeller();
        //    var orderDetails = await GenerateRandomOrder();
        //   // var details=orderDetails.Where(x=>x.ProductId==buyer.Orders.Select(x=>x.)).ToList();
        //    var order =await ClientService.CreateOrders(buyer.Id, seller.Id, orderDetails);
          
        //    if (order != null)
        //    {
        //        OnBuy?.Invoke(this, order);
                
        //    }
        //    else
        //    {
        //        await Console.Out.WriteLineAsync("не получилось создать заказ клиента");
        //    }
        //}
        /// <summary>
        /// метод для остановки таймера
        /// </summary>
        public void StopTimer()
        {
            // Остановка таймера
            _timer.Stop();
            _timer.Dispose();
        }
        /// <summary>
        /// событие срабатывающее при успешном заказа
        /// </summary>
        public event EventHandler<Order> OnBuy;


    }

}

