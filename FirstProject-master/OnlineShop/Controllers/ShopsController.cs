using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewsModel;
using System.Data;

namespace OnlineShop.Controllers
{

    public class ShopsController : Controller
    {
        private readonly IDataModulService _moduleService;
        private readonly ISellerService _sellerService;
        private readonly DbContextOptions<DataContext> _dataContext;
        private readonly IClientService _clientService;
        private readonly IStorageService _storageService;
        public ShopsController(IDataModulService modulService,IClientService clientService,DbContextOptions<DataContext> context, IStorageService storageService, ISellerService sellerService)
        {
            _clientService = clientService;
            _storageService = storageService;
            _moduleService = modulService;
            _dataContext = context;
            _sellerService = sellerService;
        }
        [HttpGet]
        //[Route("Shops")]
        public async Task<IActionResult> List()//ViewResult
        {
            ViewBag.Title = "Страница с Магазинами";

            ShopResultViewModel viewModel = new ShopResultViewModel()
            {
                GetShops = await _moduleService.GetShops()
            };
            return View(viewModel);


        }
      
        [HttpGet]
        public async Task<IActionResult> Sellers(int id)
        {
            
            var shop = await _storageService.GetShop(id);
            if(shop== null)
                return NotFound();
            var model = new ShopViewModel()
            {
                Shop = shop
            };           
            return View(model);
        }



        [HttpGet]
        public IActionResult CreateOrder(int sellerId,int productId)
        {
            var model = new OrderViewModel
            {
                SellerId = sellerId,
                BuyerId = 1081
                
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(RequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Products == null || !model.Products.Any())
            {
                ModelState.AddModelError("", "Необходимо добавить хотя бы один продукт.");
                return View(model);
            }
           var items= model.Products.Select(x=>new OrderItem { ProductId=x.ProductId,Count=x.Count,Cost=x.Cost }).ToList();
       

            var order = await _clientService.CreateOrders(model.BuyerId, model.SellerId, items);
            if (order == null)
            return View(model);

          var seller= await _storageService.GetSeller(model.SellerId);
            if(seller == null) return View(model);

            await _sellerService.ProcessOrder(order, seller.ShopId, model.SellerId);

            if (order != null)
            {
                return RedirectToAction("Order", new { id = order.Id, });
            }

            ModelState.AddModelError("", "Не удалось создать заказ");
            return View(model);
        }


        public async Task<IActionResult> Order(int id)
        {
            var order = await _clientService.GetOrder(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        [HttpGet]
        public IActionResult CreateSeller(int id)
        {
            var seller=new SellerViewModel() {ShopId=id };
            return View(seller);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeller(int shopId,SellerViewModel model)
        {
            var seller=new Seller() {Name = model.Name,ShopId=model.ShopId};
            if (ModelState.IsValid)
            {
                await _storageService.AddSeller(shopId,seller);

                return RedirectToAction("Sellers",new {id=shopId });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateShop()
        {
            ShopCreateViewModel model = new ShopCreateViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop(ShopAddViewModel model)
        {
            var shop=new Shop() {Name=model.Name };
            if (ModelState.IsValid)
            {
                await _storageService.CreateShop(shop);

                return RedirectToAction("Shop");
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ProductList(int id)
        {

          var shop= await _storageService.GetShop(id);
            var productList = new List<ProductList>();

            if (shop == null)return View(productList);
            var products = shop.Transactions.Select(x => _moduleService.Product(x.ProductId)).ToList();
            foreach (var product in products)
            {
                if(product == null) break;
                productList.Add(new ProductList()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                });
            }
            return View(productList);

        }
        [HttpGet]
        public async Task<IActionResult> OrderTransactionList(int sellerid)
        {
            var orderTransactions=await _storageService.GetOrderTransactions(sellerid);
            var viewModel = orderTransactions.Select(ot => new OrderTransactionViewModel
            {
                Id = ot.Id,
                DateCreate = ot.DateCreate,
                Cost = ot.Cost,
                OrderId = ot.OrderId
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> StorageTransactionList(int id)
        {
            var storage = await _storageService.GetStorageTransaction(id);
            var storages = storage.Select(x => new StorageTransactionViewModel { Id = x.Id,
                DateCreate = x.DateCreate,
                Count = x.Count,
                ProductId = x.ProductId,
                ShopId = id,
                TransactionType = x.TransactionType,
            }).ToList();
            return View(storages);
        }
    }
}
