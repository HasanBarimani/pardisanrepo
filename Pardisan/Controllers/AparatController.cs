using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using Pardisan.Services;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class AparatController : Controller
    {
        private readonly IAparatRepository _aparatRepository;
        public AparatController(IAparatRepository aparatRepository)
        {
            _aparatRepository = aparatRepository;
        }
        public async Task<IActionResult> Index()
        {
            var estates = await _aparatRepository.GetAll();
            return View(estates.Data);
        }
    }
}

