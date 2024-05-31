using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pardisan.Interfaces;
using Pardisan.ViewModels;
using Pardisan.ViewModels.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pardisan.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;
        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> getAll(MessageDatatableInput input)
        {

            var data = await _messageRepository.GetAllMessages(input);

            return new JsonResult(data);
        }
        [HttpPost]
        public async Task<IActionResult> GetMessageById(int Id)
        {
            var response = await _messageRepository.GetMessageById(Id);
            return new JsonResult(response);
        }
        public async Task<IActionResult> Upsert(int? id)
        {

            if (id != null || id == 0)
            {
                var pageModel = await _messageRepository.GetMessageById(id.Value);

                return View(pageModel.Data);
            }
            return View();
        }
    }
}
