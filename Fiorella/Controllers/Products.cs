using System.Linq;
using Fiorella.DataLayerAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class Products : Controller
    {
        private readonly AppDbContext _dbContext;

        public Products(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            var products = _dbContext.Products.Include(x => x.Category).Skip(4).Take(4).ToList();
            return View(products);
        }
        
        public IActionResult Load(int skip )
        {
            var products = _dbContext.Products.Include(x=>x.Category).Skip(skip).Take(4).ToList();
            return PartialView("_ProductPartial",products);
        }
    }
}