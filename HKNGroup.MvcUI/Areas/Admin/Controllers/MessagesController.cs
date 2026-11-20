using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize] 
    [Area("Admin")]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await _messageService.GetAll();
            if (messages.ResultStatus == ResultStatus.Success)
            {
                if (messages.Data?.Messages != null)
                {
                    messages.Data.Messages = messages.Data.Messages.OrderByDescending(x => x.Id).ToList();
                }
                return View(messages.Data);
            }
            if (messages.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

        public async Task<IActionResult> Checked(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            await _messageService.CheckMessage(id, "Ramazan Yılmaz");
            return RedirectToAction("Index");
        }
 
        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            await _messageService.HardDelete(id);
            return Json(new { success = true });
        }
    }
}
