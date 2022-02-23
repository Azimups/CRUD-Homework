using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataLayerAccess;
using Fiorella.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var count = 0;
            var totalPrice = 0.1;
            var basket = Request.Cookies["basket"];
            if (!string.IsNullOrEmpty(basket))
            {
                var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
                foreach (var product in products)
                {
                    count +=  product.Count;
                    totalPrice += product.Count * product.Price;
                }
                
            }
            ViewBag.BasketPrice = totalPrice;
            ViewBag.BasketCount = count;
            var bioHeader = await _dbContext.BioHeaders.SingleOrDefaultAsync();
            return View(bioHeader);
        }
    }
}