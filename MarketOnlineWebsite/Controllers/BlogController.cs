using AspNetCoreHero.ToastNotification.Abstractions;
using MarketOnlineWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace MarketOnlineWebsite.Controllers
{
    public class BlogController : Controller
    {
        private readonly dbMarketsContext _context;
        public INotyfService _INotyfService { get; }
        public BlogController(dbMarketsContext context, INotyfService inotyfService)
        {
            _context = context;
            _INotyfService = inotyfService;
        }

        [Route("/blog.html", Name = "Blog")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page < 0 ? 1 : page.Value;
            var pageSize = 6;
            var lsNews = _context.News.AsNoTracking().Where(x=>x.Active == true).OrderByDescending(x => x.CreatedDate);
            PagedList<News> models = new PagedList<News>(lsNews, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
        }

        [Route("/tin-tuc/{Alias}-{id}.html", Name = "NewsDetails")]
        public IActionResult Details(int id)
        {
            var news = _context.News.AsNoTracking().SingleOrDefault(x => x.PostId==id &&  x.Active == true);
            if(news==null)
            {
                return RedirectToAction("Index");
            }
            var lsNews = _context.News
                .AsNoTracking()
                .Where(x => x.Published == true && x.PostId != id &&  x.Active == true)
                .Take(3).OrderByDescending(x => x.CreatedDate)
                .ToList();
            ViewBag.ListNews = lsNews;
            return View(news);
        }
    }
}
