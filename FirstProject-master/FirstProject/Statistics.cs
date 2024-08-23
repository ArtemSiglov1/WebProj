using FirstProject.Interfaces;
using FirstProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    /// <summary>
    /// статистика
    /// </summary>
    public class Statistics
    {
        /// <summary>
        /// всего заказов
        /// </summary>
        public double PassedOrders { get;set; }
        /// <summary>
        /// заказов записанных в бд
        /// </summary>
        public double ThereAreOrders { get;set; }

        /// <summary>
        /// таймер для вывода количества сделанных заказов
        /// </summary>
        private System.Timers.Timer _timer;

        public Statistics(IDataModulService dataModulService)
        {
            DataModulService = dataModulService;
        }

        /// <summary>
        /// количество заказов для отсановки
        /// </summary>
        public double CountOrder { get;set; }
        /// <summary>
        /// событие для остановки клиентов
        /// </summary>
        public event EventHandler<int> OnCancel;
        /// <summary>
        /// 
        /// </summary>
        public IDataModulService DataModulService { get; set; }
        /// <summary>
        /// количество заказов в консоль 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Timer_Elapsed(object sender, EventArgs e)
        {
            Console.Clear();
            Console.WriteLine(PassedOrders);
        }
        /// <summary>
        /// включает таймер
        /// </summary>
        public void Start()
        {
            _timer = new System.Timers.Timer(1000);

            // Подписка на событие Elapsed, которое срабатывает при истечении интервала
            _timer.Elapsed += Timer_Elapsed;

            // Автоматический перезапуск таймера
            _timer.AutoReset = true;
            _timer.Start();
        }
        public void StopTimer()
        {
            // Остановка таймера
            _timer.Stop();
            _timer.Dispose();
        }
        /// <summary>
        /// меняет кол-во сделанных заказов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChangePassed(object sender, int e)
        {
            PassedOrders += e;
            if(PassedOrders ==5) {
                OnCancel?.Invoke(this,5);
            }
        }
        public void ChangeThere(object sender,int e)
        {
            ThereAreOrders += e;
        }
        public async Task StatisticsDataBase()
        {
             Console.WriteLine($"Кол-во товара по заказам:{await DataModulService.SoldOnOrders()}"  );
            Console.WriteLine($"Кол-во списанного товаров со склада:{await DataModulService.ProductShipped()}") ;
            foreach(var item in await DataModulService.GetStatisticMagazins())
            {
                Console.WriteLine($"Название магазина-{item.ShopName},\nКол-во проданного товара-{item.SellsTovar},\nСколько денег полученно за товар-{item.Cost}");
            }

        }
    }
}
