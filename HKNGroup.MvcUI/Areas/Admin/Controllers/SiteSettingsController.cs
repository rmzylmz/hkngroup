using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HKNGroup.WebUI.Areas.Admin.Helpers;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class SiteSettingsController : Controller
    {
        private readonly ISiteSettingsService _siteIdentityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SiteSettingsController(ISiteSettingsService siteIdentityService, IHttpContextAccessor httpContextAccessor)
        {
            _siteIdentityService = siteIdentityService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var identity = await _siteIdentityService.GetUpdateDto(1);
            if (identity.ResultStatus == ResultStatus.Success)
            {
                return View(identity.Data);
            }
            if (identity.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Index(SiteSettingsUpdateDto siteIdentityUpdateDto, IFormFile? fileImg)
        {
            if (ModelState.IsValid)
            { 
                try
                {
                    if (fileImg != null && fileImg.Length > 0)
                    {
                        string folder = "uploads/sliders";
                        int targetWidth = 1920;
                        int targetHeight = 1050;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, fileImg, targetWidth, targetHeight, Request);
                        siteIdentityUpdateDto.Logo = "/" + result;
                    }
                    
                    var updateResult = await _siteIdentityService.Update(siteIdentityUpdateDto);
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Site ayarları güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(siteIdentityUpdateDto);
        }

 
    }
}
