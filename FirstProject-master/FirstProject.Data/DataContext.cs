using FirstProject.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FirstProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StorageTransaction> StorageTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Server=PS-3052023\\TESTMSSQL;Database=FirstProject;User Id=test;Password=test;TrustServerCertificate=True");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Users;Username=postgres;Password=111111");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>().ToTable("Shops");
            modelBuilder.Entity<Shop>().HasKey(x => x.Id);

            modelBuilder.Entity<Buyer>().ToTable("Buyers");
            modelBuilder.Entity<Buyer>().HasKey(x => x.Id);

            modelBuilder.Entity<Seller>().ToTable("Sellers");
            modelBuilder.Entity<Seller>().HasKey(x => x.Id);
            modelBuilder.Entity<Seller>()
                   .HasOne(seller => seller.Shop)
                   .WithMany(shop => shop.Sellers)
                   .HasForeignKey(seller => seller.ShopId);


            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .HasOne(order => order.Buyer)
                   .WithMany(buyer => buyer.Orders)
                   .HasForeignKey(order => order.BuyerId);
            modelBuilder.Entity<Order>()
                .HasOne(x => x.Seller)
                .WithMany(x=>x.Orders)
                .HasForeignKey(x=>x.SellerId);
            


            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<OrderItem>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderItem>()
                .HasOne(order => order.Order)
                .WithMany(buyer => buyer.Items)
                .HasForeignKey(order => order.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.ProductId);
           // modelBuilder.Entity<OrderItem>().Property(u => u.Cost).HasComputedColumnSql("[Count*Product.Price]");


            modelBuilder.Entity<OrderTransaction>().ToTable("OrderTransactions");
            modelBuilder.Entity<OrderTransaction>().HasKey(x => x.Id);
            modelBuilder.Entity<OrderTransaction>()
    .HasOne(order => order.Order)
       .WithMany(buyer => buyer.OrderTransaction)
       .HasForeignKey(order => order.OrderId);

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>().HasKey(x => x.Id);

            modelBuilder.Entity<StorageTransaction>().ToTable("StorageTransactions");
            modelBuilder.Entity<StorageTransaction>().HasKey(x => x.Id);
            modelBuilder.Entity<StorageTransaction>().Property(x => x.TransactionType).ValueGeneratedNever();
            modelBuilder.Entity<StorageTransaction>()
                   .HasOne(transaction => transaction.Shop)
                   .WithMany(shop => shop.Transactions)
                   .HasForeignKey(seller => seller.ShopId);
            modelBuilder.Entity<StorageTransaction>()
            .HasOne(transaction => transaction.Products)
            .WithMany(shop => shop.Transactions)
            .HasForeignKey(seller => seller.ProductId);
            modelBuilder.Entity<StorageTransaction>().HasIndex(x => new {x.Id,x.ShopId});
            base.OnModelCreating(modelBuilder);
        }
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            Console.WriteLine( builder
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Name
            && level == LogLevel.Information));
        });
    }
}