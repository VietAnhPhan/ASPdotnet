using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("excel-practice")]
    public class ExcelPracticeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index([FromQuery] ExcelPracticeFilter filter)
        {
            IEnumerable<SalesRecord> rows = SalesData.All;

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                var term = filter.Search.Trim();
                rows = rows.Where(r =>
                    r.Salesperson.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    r.Product.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    r.Region.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.Region))
            {
                rows = rows.Where(r => r.Region == filter.Region);
            }

            if (!string.IsNullOrEmpty(filter.Product))
            {
                rows = rows.Where(r => r.Product == filter.Product);
            }

            if (filter.From is { } from)
            {
                rows = rows.Where(r => r.Date >= from);
            }

            if (filter.To is { } to)
            {
                rows = rows.Where(r => r.Date <= to);
            }

            var model = new ExcelPracticeViewModel
            {
                Filter = filter,
                Rows = rows.ToList(),
                Regions = SalesData.Regions,
                Products = SalesData.Products,
            };

            return View(model);
        }
    }
}
