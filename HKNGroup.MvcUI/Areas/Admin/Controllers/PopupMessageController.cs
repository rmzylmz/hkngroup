using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class PopupMessageController : Controller
    {
        private readonly IPopupMessagesService _popupMessageService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PopupMessageController(IPopupMessagesService popupMessageService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _popupMessageService = popupMessageService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
             var sliders = await _popupMessageService.GetAllByNonDeleteAndActive();
            if (sliders.ResultStatus == ResultStatus.Success)
            {
                if (sliders.Data?.PopupMessages != null)
                {
                    sliders.Data.PopupMessages = sliders.Data.PopupMessages.OrderByDescending(x => x.Id).ToList();
                }
                return View(sliders.Data);
            }
            if (sliders.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var slider = await _popupMessageService.GetUpdateDto(id);

            if (slider.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetString("imagePath", slider.Data.Thumbnail.ToString());
                return View(slider.Data);
            }
            if (slider.ResultStatus == ResultStatus.Error)
            {
                return View();
            }
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(PopupMessagesUpdateDto popupMessageUpdateDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/popupMessages";
                        int targetWidth = 1920;
                        int targetHeight = 1050;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        popupMessageUpdateDto.Thumbnail = result;
                    }
                    popupMessageUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _popupMessageService.Update(popupMessageUpdateDto, "Ramazan Yılmaz");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Popup mesajı güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(popupMessageUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(PopupMessagesAddDto popupMessageAddDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/popupMessages";
                        int targetWidth = 1920;
                        int targetHeight = 1050;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        popupMessageAddDto.Thumbnail = result;
                    }
                    popupMessageAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _popupMessageService.Add(popupMessageAddDto, "Ramazan Yılmaz");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Popup mesajı eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(popupMessageAddDto);
        }

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var slider = await _popupMessageService.Get(id);

            if (slider == null)
            {
                return NotFound();
            }

            var imagePath = slider.Data.PopupMessages.Thumbnail;

            await _popupMessageService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }

        public string imagePath { get; set; }
    }
}
