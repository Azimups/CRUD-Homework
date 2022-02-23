using System;
using System.Threading.Tasks;
using Fiorella.DataLayerAccess;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.MyAdminPanel.Controllers
{
    [Area("MyAdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var bloggers = await _dbContext.Bloggers.ToListAsync();
            return View(bloggers);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            var blogger = await _dbContext.Bloggers.FindAsync(id);
            if (blogger==null)
            {
                return NotFound();
            }

            return View(blogger);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blogger blogger)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExistBlogger = await _dbContext.Bloggers.AnyAsync(x => x.BloggerTitle.ToLower() == blogger.BloggerTitle.ToLower());
            if (isExistBlogger)
            {
                ModelState.AddModelError("BloggerTitle","Bu adda Blogger movcuddur");
                return View();
            }

            await _dbContext.Bloggers.AddAsync(blogger);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var blogger = await _dbContext.Bloggers.FindAsync(id);
            if (blogger==null)
            {
                return NotFound();
            }

            return View(blogger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Blogger blogger)
        {
            if (id==null)
            {
                return NotFound();
            }

            if (id!=blogger.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existBlogger = await _dbContext.Bloggers.FindAsync(id);
            if (existBlogger==null)
            {
                return NotFound();
            }

            var existBloggerName =
                await _dbContext.Bloggers.AnyAsync(x =>
                    x.BloggerTitle.ToLower() == blogger.BloggerTitle.ToLower() && x.Id!=id);
            if (existBloggerName)
            {
                ModelState.AddModelError("Title","Bu adda Title Movcuddur");
                return View();
            }

            existBlogger.BloggerTitle = blogger.BloggerTitle;
            existBlogger.BloggerSubTitle = blogger.BloggerSubTitle;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}