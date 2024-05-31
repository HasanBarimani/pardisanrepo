using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using Pardisan.Services;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class HoldingController : Controller
    {
        private readonly IHoldingRepository _holdingRepository;

        public HoldingController(IHoldingRepository holdingRepository)
        {
            _holdingRepository = holdingRepository;
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = await _holdingRepository.GetById(id);
            return View(model.Data);
        }
    }
}
