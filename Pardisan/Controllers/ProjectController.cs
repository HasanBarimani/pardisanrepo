using Microsoft.AspNetCore.Mvc;
using Pardisan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pardisan.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IEstateRepository _estateRepository;

        public ProjectController(IEstateRepository estateRepository)
        {
            _estateRepository = estateRepository;
        }

        public async Task<IActionResult> Index()
        {
            var estates = await _estateRepository.GetAll();
            return View(estates.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _estateRepository.GetById(id);
            return View(model.Data);
        }
    }
}
