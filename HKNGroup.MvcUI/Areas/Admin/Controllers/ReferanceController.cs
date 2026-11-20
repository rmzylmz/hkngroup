using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class ReferanceController : Controller
    {
        private readonly IReferanceService _referanceeService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReferanceController(IReferanceService referanceeService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _referanceeService = referanceeService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var Referances = await _referanceeService.GetAllByNonDeleteAndActive();
            if (Referances.ResultStatus == ResultStatus.Success)
            {
                if (Referances.Data?.Referances != null)
                {
                    Referances.Data.Referances = Referances.Data.Referances.OrderByDescending(x => x.Id).ToList();
                }
                return View(Referances.Data);
            }
            if (Referances.ResultStatus == ResultStatus.Error)
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
            var Referance = await _referanceeService.GetUpdateDto(id);

            if (Referance.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetString("imagePath", Referance.Data.Thumbnail.ToString());
                return View(Referance.Data);
            }
            if (Referance.ResultStatus == ResultStatus.Error)
            {
                return View();
            }
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(ReferanceUpdateDto referanceUpdateDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/referans";
                        int targetWidth = 391;
                        int targetHeight = 220;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        referanceUpdateDto.Thumbnail = result;
                    }
                    referanceUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _referanceeService.Update(referanceUpdateDto, "Ramazan Yılmaz");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Referans güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(referanceUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(ReferanceAddDto referanceAddDto, IFormFile? Thumbnail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    if (Thumbnail != null && Thumbnail.Length > 0)
                    {
                        string folder = "uploads/referans";
                        int targetWidth = 247;
                        int targetHeight = 233;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, Thumbnail, targetWidth, targetHeight, Request);
                        referanceAddDto.Thumbnail = result;
                    }
                    referanceAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _referanceeService.Add(referanceAddDto, "Ramazan Yılmaz");
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Referans eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(referanceAddDto);
        }

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var Referance = await _referanceeService.Get(id);

            if (Referance == null || Referance.ResultStatus != ResultStatus.Success || Referance.Data?.Referances == null)
            {
                return NotFound();
            }

            var imagePath = Referance.Data.Referances.Thumbnail;

            await _referanceeService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }

        public string imagePath { get; set; }
    }
}
