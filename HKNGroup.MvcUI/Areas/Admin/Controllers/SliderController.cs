using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SliderController(ISliderService sliderService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _sliderService = sliderService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllByNonDeleteAndActive();
            if (sliders.ResultStatus == ResultStatus.Success)
            {
                if (sliders.Data?.Sliders != null)
                {
                    sliders.Data.Sliders = sliders.Data.Sliders.OrderByDescending(x => x.Id).ToList();
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
            var slider = await _sliderService.GetUpdateDto(id);

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
        public async Task<IActionResult> Edit(SliderUpdateDto sliderUpdateDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/sliders";
                        int targetWidth = 1920;
                        int targetHeight = 1050;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        sliderUpdateDto.Thumbnail = result;
                    }
                    sliderUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _sliderService.Update(sliderUpdateDto, "Ramazan Yılmaz");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Slider güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(sliderUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(SliderAddDto sliderAddDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/sliders";
                        int targetWidth = 1920;
                        int targetHeight = 1050;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        sliderAddDto.Thumbnail = result;
                    }
                    sliderAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _sliderService.Add(sliderAddDto, "Ramazan Yılmaz");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Slider eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(sliderAddDto);
        }

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var slider = await _sliderService.Get(id);

            if (slider == null || slider.ResultStatus != ResultStatus.Success || slider.Data?.Slider == null)
            {
                return NotFound();
            }

            var imagePath = slider.Data.Slider.Thumbnail;

            await _sliderService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }

        public string imagePath { get; set; }
    }
}
