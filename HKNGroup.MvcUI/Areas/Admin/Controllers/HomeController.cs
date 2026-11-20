using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IMessageService _messageService;
        private readonly IAdminService _adminService;
        private readonly IProjectService _projectService;
        private readonly IArticleService _articleService;
        private readonly IPageService _pageService;
        private readonly IOurServicesService _ourServicesService;
        private readonly IReferanceService _referanceService;
        private readonly ISiteSettingsService _siteSettingsService;

        public HomeController(ISliderService sliderService, IMessageService messageService, IAdminService adminService, IProjectService projectService, IArticleService articleService, IPageService pageService, IOurServicesService ourServicesService, IReferanceService referanceService, ISiteSettingsService siteSettingsService)
        {
            _sliderService = sliderService;
            _messageService = messageService;
            _adminService = adminService;
            _projectService = projectService;
            _articleService = articleService;
            _pageService = pageService;
            _ourServicesService = ourServicesService;
            _referanceService = referanceService;
            _siteSettingsService = siteSettingsService;
        }

        public async Task<IActionResult> Index()
        {

            HttpContext.Session.Remove("imagePath");
            var sliders = await _sliderService.GetAll();
            var messages = await _messageService.GetAll();
            ViewBag.SliderCount = sliders.Data.Sliders.Count;
            ViewBag.MessageCount = messages.Data.Messages.Count;

            var tamamlananProje = await _projectService.GetAllByNonDeleteAndActiveWithProjectCategoryId(5);
            var planlananProje = await _projectService.GetAllByNonDeleteAndActiveWithProjectCategoryId(6);
            var devamEdenProje = await _projectService.GetAllByNonDeleteAndActiveWithProjectCategoryId(7);
            var proje = await _projectService.GetAllByNonDeleteAndActive();

            var makalaler = await _articleService.GetAllByNonDeleteAndActive();
            var hizmetler = await _ourServicesService.GetAllByNonDeleteAndActive();
            var sayfalar = await _pageService.GetAllByNonDeleteAndActive();
            var referanslar = await _referanceService.GetAllByNonDeleteAndActive();


            ViewBag.TamamlananProje = tamamlananProje.Data.Projects.Count;
            ViewBag.PlanlananProje = planlananProje.Data.Projects.Count;
            ViewBag.DevamEdenProje = devamEdenProje.Data.Projects.Count;
            ViewBag.ToplamProje = proje.Data.Projects.Count;

            ViewBag.AktifMakale = makalaler.Data.Articles.Count;
            ViewBag.Hizmetler = hizmetler.Data.OurServices.Count;
            ViewBag.Sayfalar = sayfalar.Data.Page.Count;

          
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Referances = referanslar.Data.Referances;
            homeViewModel.Articles = makalaler.Data.Articles;
            homeViewModel.Projects = proje.Data.Projects;
            var siteSettings = await _siteSettingsService.GetUpdateDto(1);
            if (siteSettings.ResultStatus == ResultStatus.Success)
            {
                homeViewModel.SiteSetting = siteSettings.Data;
            }
            return View(homeViewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn", "Auth");
        }
        public async Task<IActionResult> Setting()
        {
            var admin = await _adminService.Get(1);
            if (admin.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (admin.ResultStatus == ResultStatus.Success)
            {
                return View(admin.Data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> EditAdmin(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var admin = await _adminService.GetUpdateDto(id);
            if (admin.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (admin.ResultStatus == ResultStatus.Success)
            {
                return View(admin.Data);
            }
            return NotFound();
        }

                [HttpPost]
        public async Task<IActionResult> EditAdmin(AdminUpdateDto adminUpdateDto, string oldPsswrd, string oldSQHash)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updateResult = await _adminService.Update(adminUpdateDto, "Ramazan Yılmaz");
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        if (!string.IsNullOrWhiteSpace(adminUpdateDto.UserName))
                        {
                            var identity = (System.Security.Claims.ClaimsIdentity?)User.Identity;
                            if (identity != null)
                            {
                                var existingClaim = identity.FindFirst("Username");
                                if (existingClaim != null) identity.RemoveClaim(existingClaim);
                                identity.AddClaim(new System.Security.Claims.Claim("Username", adminUpdateDto.UserName));

                                var existingNameClaim = identity.FindFirst(System.Security.Claims.ClaimTypes.Name);
                                if (existingNameClaim != null) identity.RemoveClaim(existingNameClaim);
                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, adminUpdateDto.UserName));

                                await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new System.Security.Claims.ClaimsPrincipal(identity));
                            }
                        }
                        return RedirectToAction("Setting");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Yönetici bilgileri güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(adminUpdateDto);
        }
    }
}
