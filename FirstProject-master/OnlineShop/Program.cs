using FirstProject.Data;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop;


CreateWedHost(args).Build().Run();
static IWebHostBuilder CreateWedHost(string[] args) => WebHost.CreateDefaultBuilder(args)
    .UseStartup<Startup>();

