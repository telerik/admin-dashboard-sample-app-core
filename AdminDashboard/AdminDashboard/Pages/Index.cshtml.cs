using AdminDashboard.Data;
using AdminDashboard.Data.Models.Employees;
using AdminDashboard.Data.Models.Sales;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public JsonResult OnPostGetSales(int type)
        {
            var salesByRegion = _context.Sales
                                .Where(sale => sale.TransactionDate > new DateTime(2019, 1, 1))
                                .Where(sale => sale.TransactionDate < new DateTime(2020, 1, 1))
                                .GroupBy(sale => new { sale.Region, sale.TransactionDate.Year, sale.TransactionDate.Month })
                                .Select(group => new SalesByDateViewModel
                                {
                                    Date = new DateTime(group.Key.Year, group.Key.Month, 1),
                                    Region = group.Key.Region,
                                    Sum = type != 1 ? group.Sum(sale => sale.Amount) : group.Count()
                                });

            return new JsonResult(salesByRegion.ToList());
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request, int team)
        {
            var employees = _context.Employees.AsQueryable();
            
            if (team != 1)
            {
                employees = employees.Where(x => x.JobTitle.Contains("Engineer"));
            }

            var data = employees.Select(x => new EmployeeViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                JobTitle = x.JobTitle,
                Rating = x.Rating,
                Budget = x.Budget
            }).ToList();
            return new JsonResult(data.ToDataSourceResult(request));
        }
    }
}
