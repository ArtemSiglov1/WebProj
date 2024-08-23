using FirstProject;
using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.BussinesLogic;
using FirstProject.Service;
using FirstProject.Service.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

///
internal class Program
{
    private static async Task Main(string[] args)
    {
        ///
        var configuration = new ConfigurationBuilder().Build();
        ///
        var services = new ServiceCollection();
        ///
        services.AddDbContext<DataContext>(x =>
            x.UseSqlServer(configuration.GetConnectionString("DataContext")));
        ///
        services.AddTransient<IDataModulService, DataService>();
        services.AddTransient<ISellerService, SellerService>();
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IStorageService, StorageService>();
        ///

        var provider = services.BuildServiceProvider();
        ///
        IStorageService storageService = provider.GetService<IStorageService>();
        IClientService clientService = provider.GetService<IClientService>();
        IDataModulService dataService = provider.GetService<IDataModulService>();
        ISellerService sellerService = provider.GetService<ISellerService>();
    //    optionsBuilder.UseSqlServer(
    //connectionString,
    //sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()
//);
        ///
        Manager manager = new Manager(storageService, clientService, dataService, sellerService);

        ///
        //await manager.DataService.DelleteAll();
        /////
        //await manager.DataService.InitData(storageService);
        /////
        //manager.InitClient(1);
        //await manager.InitMagazin();

        /////
        ////shops = manager.InitShop();
        //manager.Start();
        /////
     
        //Console.ReadKey();
    }
}
//List<OrderItem> orderItems = new List<OrderItem>
//    {
//        new OrderItem { ProductId = 1 },
//        new OrderItem { ProductId = 2}
//    };

//BaseResponse response = await manager.Shop_OrderProduct(orderItems, 1);
//Console.WriteLine(response.ErrorMessage);






// Mock implementations of interfaces and classes for demonstration purposes










//"Host=localhost;Port=5432;Database=UsersLunguage;Username=postgres;Password=111111";
//"Server=PS-3052023\TESTMSSQL;Port=1433;Database=TestLesson;Username=test;Password=test";
//services.AddTransient<UserService>();

//services.AddTransient<RoleService>();
//services.AddTransient<ProfessionService>();

//var sp = services.BuildServiceProvider();
