using FirstProject.Data;
using FirstProject.Data.Models;
using FirstProject.Interfaces;
using FirstProject.Service;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewsModel;
using System.Data;
using System.Drawing;

namespace OnlineShop.Controllers
{

    public class ShopsController : Controller
    {
        IDataModulService _moduleService;
        ISellerService _sellerService;
        DbContextOptions<DataContext> _dataContext;
        IClientService _clientService;
        IStorageService _storageService;
        public ShopsController(IDataModulService modulService,IClientService clientService,DbContextOptions<DataContext> context, IStorageService storageService, ISellerService sellerService)
        {
            _clientService = clientService;
            _storageService = storageService;
            this._moduleService = modulService;
            _dataContext = context;
            this._sellerService = sellerService;
        }
        [HttpGet]
        //[Route("Shops")]
        public async Task<IActionResult> Shop()//ViewResult
        {
            ViewBag.Title = "Страница с Магазинами";

            ShopResultViewModel viewModel = new ShopResultViewModel()
            {
                GetShops = await _moduleService.GetShop()
            };
            return View(viewModel);


        }
      
        [HttpGet]
        public async Task<IActionResult> Sellers(int id)
        {
            ShopViewModel model = new ShopViewModel();
            model.Shop = await _storageService.GetShop(id);
            
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult CreateOrder(int sellerId)
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
           var items= model.Products.Select(x=>new OrderItem { ProductId=x.ProductId,Count=x.Count,Cost=x.Cost,Product= _moduleService.Product(x.ProductId) }).ToList();
            

            var order = await _clientService.CreateOrders(model.BuyerId, model.SellerId, items);

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
    }
}
