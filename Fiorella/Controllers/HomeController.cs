using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataLayerAccess;
using Fiorella.Models;
using Fiorella.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            HttpContext.Session.SetString("session","SessionValue");
            
            var sliderImages = _dbContext.SliderImages.ToList();
            var slider = _dbContext.Sliders.SingleOrDefault();

            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.Include(x => x.Category).ToList();

            var about = _dbContext.AboutImg.SingleOrDefault();
            var aboutText = _dbContext.AboutTexts.SingleOrDefault();

            var experts = _dbContext.Experts.ToList();
            var expertTitle = _dbContext.ExpertTitles.SingleOrDefault();

            var blogTitle = _dbContext.BlogTitles.SingleOrDefault();
            var bloggers = _dbContext.Bloggers.ToList();

            var saysliders = _dbContext.SaySliders.ToList();

            var subscribeImg = _dbContext.SubscribeImg.SingleOrDefault();

            var instagramImgs = _dbContext.InstagramImg.ToList();
            
            
            
            return View(new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products, 
                About = about,
                AboutText = aboutText,
                Experts = experts,
                ExpertTitle = expertTitle,
                BlogTitle = blogTitle,
                Bloggers = bloggers,
                SaySliders = saysliders,
                SubscribeImg = subscribeImg,
                InstagramImgs = instagramImgs,
            });
        }

        public async Task<IActionResult> Basket()
        {
            //var session = HttpContext.Session.GetString("session");
            //return Content(session);
            var basket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basket))
            {
                return Content("Empty");
            }
            var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
            var newBasket = new List<BasketViewModel>();
            foreach (var basketViewModel in basketViewModels)
            {
                var product = await _dbContext.Products.FindAsync(basketViewModel.Id);
                if (product==null)
                {
                    continue;
                }
                else
                {
                    newBasket.Add(new BasketViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Image = product.Image,
                        Price = product.Price,
                        Count=basketViewModel.Count
                    });
                }
                basket = JsonConvert.SerializeObject(newBasket);
                Response.Cookies.Append("basket",basket);
            }
            return View(newBasket);
        }

        public async Task<IActionResult> AddToBasket(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            var product = await _dbContext.Products.FindAsync(id);
            if (product==null)
            {
                return NotFound();
            }

            List<BasketViewModel> basketViewModels;
            var existBasket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(existBasket))
            {
                basketViewModels = new List<BasketViewModel>();
            }
            else
            {
                basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(existBasket);
            }

            var existBasketViewMocdel = basketViewModels.FirstOrDefault(x => x.Id == id);

            if (existBasketViewMocdel==null)
            {
                existBasketViewMocdel = new BasketViewModel()
                {
                    Id=product.Id,
                };
                basketViewModels.Add(existBasketViewMocdel);
            }
            else
            {
                existBasketViewMocdel.Count++; 
            }
            var basket = JsonConvert.SerializeObject(basketViewModels);
            Response.Cookies.Append("basket",basket);
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult AddProduct(int? id)
        {
            var basket = Request.Cookies["basket"];

            if (id == null)
                return BadRequest();

            if (string.IsNullOrEmpty(basket))
                return BadRequest();

            var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);

            foreach (var item in products)
            {

                if (item.Id == id)
                {
                    item.Count++;
                }
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction(nameof(Basket));
        }
        public IActionResult RemoveProduct(int? id)
        {
            var basket = Request.Cookies["basket"];

            if (id == null)
                return BadRequest();

            if (string.IsNullOrEmpty(basket))
                return BadRequest();

            var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);

            foreach (var product in products)
            {
                
                if (product.Id == id)
                {
                    product.Count--;
                    if (product.Count==0)
                    {
                        products = products.Where(x => x.Id != id).ToList();
                    }
                }
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction(nameof(Basket));
        }
        public IActionResult DeleteProduct(int? id)
        {
            var basket = Request.Cookies["basket"];

            if (id == null)
                return BadRequest();

            if (string.IsNullOrEmpty(basket))
                return BadRequest();

            var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);

            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    products = products.Where(x => x.Id != id).ToList();
                }
            }
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction(nameof(Basket));
        }
    }
}