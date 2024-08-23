using FirstProject.Data;
using FirstProject.Interfaces;
using FirstProject.Service;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop
{
    public class Startup
    {
        private IConfigurationRoot root;

        public Startup(IWebHostEnvironment hosting)
        {
            root = new ConfigurationBuilder()
                .SetBasePath(hosting.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(root.GetConnectionString("DefaultConnection")));

            //services.AddTransient<ICars, CarRepository>();
            //services.AddTransient<ICarsCategory, CategoryRepository>();
            //services.AddTransient<ICars, MockCars>();
            //services.AddTransient<ICarsCategory, MockCategory>();
            services.AddTransient<IDataModulService, DataService>();
            services.AddTransient<IStorageService, StorageService>();
            services.AddTransient<ISellerService, SellerService>();
            services.AddTransient<IClientService, ClientService>();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Shops}/{action=Shop}/{id?}");
				//endpoints.MapControllerRoute(
    //                name: "default",
    //                pattern: "{controller=Shops}/{action=Result}");
            });
        }
    }
}
