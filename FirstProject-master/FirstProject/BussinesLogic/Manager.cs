using FirstProject.BussinesLogic;
using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;

namespace FirstProject.Service
{
    /// <summary>
    /// класс для работы с консолью 
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="storageService">интерфейс сервиса склада</param>
        /// <param name="clientService">интерфейс сервиса клиента</param>
        /// <param name="dataService">интерфейс сервиса бд</param>
        /// <param name="sellerService">интерфейс сервиса продавца</param>
        public Manager(IStorageService storageService, IClientService clientService, IDataModulService dataService, ISellerService sellerService)
        {
            StorageService = storageService;
            ClientService = clientService;
            DataService = dataService;
            SellerService = sellerService;
            Stat=new Statistics(dataService);

        }
        /// <summary>
        /// интерфейс сервиса склада
        /// </summary>
        public IStorageService StorageService { get; set; }
        /// <summary>
        /// интерфейс сервиса клиента
        /// </summary>
        public IClientService ClientService { get; set; }
        /// <summary>
        /// интерфейс сервиса бд
        /// </summary>
        public IDataModulService DataService { get; set; }
        /// <summary>
        /// интерфейс сервиса продавца
        /// </summary>
        public ISellerService SellerService { get; set; }
        /// <summary>
        /// статистика заказов 
        /// </summary>
        public Statistics Stat { get; set; }
        /// <summary>
        /// проводник 
        /// </summary>
        public Provider Provider { get; set; }
        /// <summary>
        /// список клиентов
        /// </summary>
        public List<Client> Clients { get; set; }
        /// <summary>
        /// список магазинов
        /// </summary>
        public List<Magazin> Magazins { get; set; }
        /// <summary>
        /// метод работающий по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public async void Stat_OnCancel(object sender, int e)
        {
           
                foreach (var client in Clients)
                {
                    client.StopTimer();
                }
            Stat.StopTimer();
            await Stat.StatisticsDataBase();
            }
        /// <summary>
        /// запуск таймера
        /// </summary>
        public void Start()
        {
           
            foreach (var client in Clients)
            {
                client.OnBuy += Client_OnBuy;
                
                //client.StartTimer(1000);
            }
            Stat.Start();
            Stat.OnCancel += Stat_OnCancel;
        }
        /// <summary>
        /// добавление клиента
        /// </summary>
        /// <param name="count"></param>
        public void InitClient(int count)
        {
            Clients = new List<Client>();
            for (int i = 0; i < count; i++)
                Clients.Add(new Client(ClientService, DataService) {ClientService=ClientService,DataModulService=DataService, BuyerId = DataService.GetRandomBuyer().Id });
        }
        public async Task InitMagazin()
        {
            Magazins = new List<Magazin>();
            var shops = await DataService.GetShop();
            Magazins.AddRange(shops.Select(x => new Magazin(DataService, x.Id, SellerService)
            {
                Sellers = x.Sellers.Select(seller => new BusinessSeller() { Id = seller.Id, ShopId = x.Id }).ToList()
            }).ToList());
            foreach (var shop in Magazins)
            {
                
                    shop.DeliverRequest += Shop_OrderProduct;
                    shop.OnPassedOrders += Stat.ChangePassed;
                    shop.OnThereAreOrders += Stat.ChangeThere;
                
            }
            return;
        }
        /// <summary>
        /// клиент купил ил нет 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="order"></param>
        public async void Client_OnBuy(object sender, Order order)
        {
            //todo найти магазин по айди обработать заказ 
            // await using var db = new DataContext(_dbContextOptions);
            var magazin = Magazins.Where(x => x.Sellers.Any(s => s.Id == order.SellerId)).FirstOrDefault();
            await magazin.ProcessOrder(order);
           
        }
        public event EventHandler<StorageTransaction> TransactionReceived;
        public void Shop_OrderProduct(object sender,Order order)
        {
            var magazin = Magazins.Where(x => x.Sellers.Any(s => s.Id == order.SellerId)).FirstOrDefault();
            foreach (var item in order.Items) {
                
                StorageService.DeliverGoods(magazin.Id, item.ProductId, item.Count);
            } 
        }
    }
}
