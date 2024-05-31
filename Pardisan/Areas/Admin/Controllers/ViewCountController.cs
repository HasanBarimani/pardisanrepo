using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pardisan.Data;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ViewCountController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ViewCountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Count(DateTime from, DateTime to)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            DateTime fromDate = new DateTime(from.Year, from.Month, from.Day, persianCalendar);
            DateTime toDate = new DateTime(to.Year, to.Month, to.Day, persianCalendar);
            var resualt = _context.SiteVisit.Where(x => x.Date > fromDate && x.Date < toDate).Count();




            return new JsonResult(resualt);
        }
        public IActionResult Data()
        {

            var today = DateTime.Today;
            var lastMonth = today.AddMonths(-1);
            var data = _context.SiteVisit.OrderBy(x => x.Date).ToList();
            var viewCounts = data.Where(vc => vc.Date >= lastMonth && vc.Date <= today)
                .GroupBy(vc => vc.Date.Date)
                .Select(g => new { Day = g.Key.ToPersianDateTextify(false), Counts = g.Count() })
                .ToList();

            return new JsonResult(viewCounts);
        }
    }
}
