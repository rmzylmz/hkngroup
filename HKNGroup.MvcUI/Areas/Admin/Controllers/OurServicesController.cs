using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class OurServicesController : Controller
    {
        private readonly IOurServicesService _services;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OurServicesController(IOurServicesService services, IHttpContextAccessor httpContextAccessor)
        {
            _services = services;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var servicess = await _services.GetAllByNonDeleteAndActive();
            if (servicess.ResultStatus == ResultStatus.Success)
            {
                if (servicess.Data?.OurServices != null)
                {
                    servicess.Data.OurServices = servicess.Data.OurServices.OrderByDescending(x => x.Id).ToList();
                }
                return View(servicess.Data);
            }
            if (servicess.ResultStatus == ResultStatus.Error)
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
            var services = await _services.GetUpdateDto(id);
            if (services.ResultStatus == ResultStatus.Success)
            {
                //HttpContext.Session.SetString("imagePath", services.Data.Images.ToString());
                return View(services.Data);
            }
            if (services.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(OurServicesUpdateDto servicesUpdateDto, IFormFile? fileImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (fileImg != null && fileImg.Length > 0)
                    {
                        string folder = "uploads/hizmetler";
                        int targetWidth = 391;
                        int targetHeight = 230;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, fileImg, targetWidth, targetHeight, Request);
                        servicesUpdateDto.Images = result;
                    }
                    servicesUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _services.Update(servicesUpdateDto);
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Hizmet güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(servicesUpdateDto);
        }
        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _services.Get(id);

            if (service == null || service.ResultStatus != ResultStatus.Success || service.Data?.OurServices == null)
            {
                return NotFound();
            }

            var imagePath = service.Data.OurServices.Images;

            await _services.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }

            return Json(new { success = true });
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(OurServicesAddDto servicesAddDto, IFormFile? fileImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (fileImg != null && fileImg.Length > 0)
                    {
                        string folder = "uploads/hizmetler";
                        int targetWidth = 391;
                        int targetHeight = 230;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, fileImg, targetWidth, targetHeight, Request);
                        servicesAddDto.Images = result;
                    }
                    servicesAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _services.Add(servicesAddDto);
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Hizmet eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(servicesAddDto);
        }


    }
}
