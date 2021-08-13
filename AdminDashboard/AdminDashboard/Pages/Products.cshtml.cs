using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public JsonResult OnPostPerformanceData()
        {
            var totalSales = _context.Sales.Count();
            var performanceData = _context.Sales
                                .GroupBy(sale => new { sale.ProductGroup, sale.Region })
                                .Select(group => new
                                {
                                    Product = group.Key.ProductGroup,
                                    Region = group.Key.Region,
                                    Amount = _context.Sales.Where(x => x.ProductGroup == group.Key.ProductGroup && x.Region == group.Key.Region).Sum(x => x.Amount),
                                    SalesCount = _context.Sales.Where(x => x.ProductGroup == group.Key.ProductGroup && x.Region == group.Key.Region).Count(),
                                    Rate = _context.Sales.Where(x => x.ProductGroup == group.Key.ProductGroup && x.Region == group.Key.Region).Sum(x => x.Amount) / _context.Sales.Where(x => x.ProductGroup == group.Key.ProductGroup && x.Region == group.Key.Region).Count() * 100
                                });

            return new JsonResult(performanceData.ToList());
        }

        public JsonResult OnPostProducts()
        {
            var Titles = new string[] {
                "All-in-One Mac",
                "Colorful Future with SSG500",
                "The Prestige Series",
                "Designs into Websites",
                "Out of the Shadow",
                "Design Solutions",
                "Remote Life",
                "Through the Lens",
                "Wireless Culture",
                "Changing the Game",
                "Thin Tech",
                "Mobile Edu"
            };
            var Descriptions = new string[]
            {
                "An alternative solution for your business. Get our latest offer now and save up to $300 with the terrific All-in-One Mac laptop!",
                "Add some color to the dull and grey everyday life with our latest  laptop offer! SSG500 will bring your creative ideas to life!",
                "The SA325 Prestige Series deliver an excellent audio performance, combined with the best noise cancellation and a ton of additional features.",
                "Advance the web development process with Artificial Design Intelligence to deliver the best user experience directly through your website.",
                "This is the right place if you’re trying to reduce your Shadow IT risk. Explore our latest guide on how to protect and manage your software assets.",
                "This is the right place if you’re looking for a new laptop design. Spice up your environment with our fresh ideas for laptop skins, stickers, and sleeves.",
                "Smart phones of the future are coming our way! Get a glimpse of how they might develop and what to expect from their functionalities.",
                "Look forward to the future in a different light with AI. Enjoy better, faster, and more intuitive photos in the 3D world.",
                "Listen to your favorite genres with style! Our latest headphone design offers excellent wireless audio for a better music experience.",
                "The future of the headphone technology is wireless. Get to know the latest trends for fashionable designs and improvements in the headphone world.",
                "The changing technology and economy are driving a new world of mobile computing. Explore our super-thin designs with folding displays.",
                "Harness the nature of the mobile phone with our latest Mobile Edu design, created to assist you with your studies for a more informal approach to learning.",
            };
            var products = Enumerable.Range(1, 50).Select(x => new
            {
                ProductID = x,
                Title = Titles[x%12],
                Description = Descriptions[x % 12]
            });

            return new JsonResult(products.ToList());
        }
    }
}
