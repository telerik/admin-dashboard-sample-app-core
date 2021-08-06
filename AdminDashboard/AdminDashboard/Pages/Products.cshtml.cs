using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminDashboard.Pages
{
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
            var products = Enumerable.Range(1, 50).Select(x => new
            {
                ProductID = x,
                Title = "Title",
                Description = "The right place to be if you're in love with high quality espresso or tea. We offer wide range to top coffee brands as Davidoff Cafe, Lavazza, Tchibo, Illy."
            });

            return new JsonResult(products.ToList());
        }
    }
}
