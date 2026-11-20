using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class ProjectCategoryController : Controller
    {
        private readonly IProjectCategoryService _projectCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectCategoryController(IProjectCategoryService projectCategoryService, IHttpContextAccessor httpContextAccessor)
        {
            _projectCategoryService = projectCategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            if (categories.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (categories.ResultStatus == ResultStatus.Success)
            {
                if (categories.Data?.ProjectCategories != null)
                {
                    categories.Data.ProjectCategories = categories.Data.ProjectCategories.OrderByDescending(x => x.Id).ToList();
                }
                return View(categories.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

                [HttpPost]
        public async Task<IActionResult> Add(ProjectCategoryAddDto categoryAddDto, IFormFile? fileImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (fileImg != null && fileImg.Length > 0)
                    { 
                        string folder = "uploads/proje-kategori";
                        int targetWidth = 391;
                        int targetHeight = 420;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, fileImg, targetWidth, targetHeight, Request);
                        categoryAddDto.CoverPhoto = result;
                    }
                    categoryAddDto.siteBaseUrl = BaseUrl;

                    var addResult = await _projectCategoryService.Add(categoryAddDto);
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", addResult.Message ?? "Kategori eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(categoryAddDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var category = await _projectCategoryService.GetUpdateDto(id);
            if (category.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (category.ResultStatus == ResultStatus.Success)
            {
                HttpContext.Session.SetString("imagePath", category.Data.CoverPhoto.ToString());
                return View(category.Data);
            }
            return NotFound();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(ProjectCategoryUpdateDto categoryUpdateDto, IFormFile? fileImg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";
                    if (fileImg != null && fileImg.Length > 0)
                    { 
                        string folder = "uploads/proje-kategori";
                        int targetWidth = 391;
                        int targetHeight = 420;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, fileImg, targetWidth, targetHeight, Request);
                        categoryUpdateDto.CoverPhoto = result;
                    }
                    categoryUpdateDto.siteBaseUrl = BaseUrl;

                    var updateResult = await _projectCategoryService.Update(categoryUpdateDto);
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Kategori güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(categoryUpdateDto);
        } 

        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _projectCategoryService.Get(id);

            if (service == null || service.ResultStatus != ResultStatus.Success || service.Data?.ProjectCategory == null)
            {
                return NotFound();
            }

            var imagePath = service.Data.ProjectCategory.CoverPhoto;

            await _projectCategoryService.HardDelete(id);

            if (!string.IsNullOrEmpty(imagePath))
            {
                ImageHelper.DeleteImage(imagePath);
            }
         
            return Json(new { success = true });
        }
    }
}
