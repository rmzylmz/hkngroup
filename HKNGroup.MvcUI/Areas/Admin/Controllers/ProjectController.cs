using HKNGroup.WebUI.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HKNGroup.WebUI.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectImagesService _projectImagesService;
        private readonly IProjectCategoryService _projectCategoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        List<ProjectImages> projectImagesList = new List<ProjectImages>();
        public ProjectController(IProjectService projectService, IProjectImagesService projectImagesService, IProjectCategoryService projectCategoryService, IHttpContextAccessor httpContextAccessor)
        {
            _projectService = projectService;
            _projectImagesService = projectImagesService;
            _projectCategoryService = projectCategoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetAllByNonDeleteAndActive();
            if (projects.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (projects.ResultStatus == ResultStatus.Success)
            {
                if (projects.Data?.Projects != null)
                {
                    projects.Data.Projects = projects.Data.Projects.OrderByDescending(x => x.Id).ToList();
                }
                return View(projects.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();

            ViewBag.ProjectCategoryList = categories.Data.ProjectCategories;
            return View();
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Add(ProjectAddDto projectAddDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    if (projectAddDto.FormFileCoverImageUrl != null)
                    {
                        string folder = "uploads/projeler";
                        int targetWidth = 391;
                        int targetHeight = 220;
                        var result = await ImageHelper.ResizeAndUploadImage(folder, projectAddDto.FormFileCoverImageUrl, targetWidth, targetHeight, _httpContextAccessor.HttpContext.Request);
                        string imageUrl = result;

                        projectAddDto.siteBaseUrl = BaseUrl;
                        projectAddDto.CoverImageUrl = imageUrl;
                    }
                    else
                    {
                        projectAddDto.siteBaseUrl = BaseUrl;
                    }

                    if (projectAddDto.GalleryFiles != null && projectAddDto.GalleryFiles.Any())
                    {
                        string folder = "uploads/projeler/gallery";

                        projectAddDto.Gallery = new List<ProjectImagesAddDto>();

                        int targetWidth = 390;
                        int targetHeight = 420;
                        foreach (var files in projectAddDto.GalleryFiles)
                        {
                            var result = await ImageHelper.ResizeAndUploadImage(folder, files, targetWidth, targetHeight, _httpContextAccessor.HttpContext.Request);
                            string imageUrl = result;

                            var gallery = new ProjectImagesAddDto()
                            {
                                Name = files.FileName,
                                URL = imageUrl,
                                siteBaseUrl = BaseUrl
                            };
                            projectAddDto.Gallery.Add(gallery);
                        }
                    }
                    else
                    {
                        projectAddDto.siteBaseUrl = BaseUrl;
                    }

                    projectAddDto.SiteId = Convert.ToInt32(HttpContext.Session.GetString("SiteId"));
                    var addResult = await _projectService.Add(projectAddDto);
                    if (addResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", addResult.Message ?? "Proje eklenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            ViewBag.ProjectCategoryList = categories.Data.ProjectCategories;
            return View(projectAddDto);
        }


        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }
            var project = await _projectService.Get(id);
            if (project.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            if (project.ResultStatus == ResultStatus.Success)
            {
                return View(project.Data);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var project = await _projectService.GetUpdateDto(id);

            if (project.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }

            if (project.ResultStatus == ResultStatus.Success)
            {
                if (project.Data.CoverImageUrl != null)
                {
                    HttpContext.Session.SetString("imagePath", project.Data.CoverImageUrl.ToString());
                }

                var projectImage = await _projectImagesService.GetAllProjectImagesId(id);
                project.Data.ProjectImages = projectImage.Data.ProjectImages;

                var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
                ViewBag.ProjectCategoryList = categories.Data.ProjectCategories;

                return View(project.Data);
            }

            return NotFound();
        }

                [HttpPost]
        public async Task<IActionResult> Edit(ProjectUpdateDto projectUpdateDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _httpContextAccessor.HttpContext.Request;
                    string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

                    var orginalCoverImageUrl = projectUpdateDto.CoverImageUrl;

                    if (projectUpdateDto.FormFileCoverImageUrl == null)
                    {
                        projectUpdateDto.CoverImageUrl = orginalCoverImageUrl;
                        projectUpdateDto.siteBaseUrl = BaseUrl;
                    }
                    else
                    {
                        await HandleCoverImageUpdate(projectUpdateDto);
                    }

                    if (projectUpdateDto.GalleryFiles != null && projectUpdateDto.GalleryFiles.Any())
                    {
                        await HandleGalleryUpdate(projectUpdateDto);
                    }

                    var updateResult = await _projectService.Update(projectUpdateDto);
                    if (updateResult.ResultStatus == ResultStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", updateResult.Message ?? "Proje güncellenirken bir hata oluştu.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var projectImage = await _projectImagesService.GetAllProjectImagesId(projectUpdateDto.Id);
            if (projectImage.ResultStatus == ResultStatus.Success && projectImage.Data != null)
            {
                projectUpdateDto.ProjectImages = projectImage.Data.ProjectImages;
            }
            var categories = await _projectCategoryService.GetAllByNonDeleteAndActive();
            ViewBag.ProjectCategoryList = categories.Data?.ProjectCategories;
            return View(projectUpdateDto);
        }

        private async Task HandleCoverImageUpdate(ProjectUpdateDto projectUpdateDto)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

            if (HttpContext.Session.GetString("imagePath") != null)
            {
                string? imagePath = HttpContext.Session.GetString("imagePath");
                if (!string.IsNullOrEmpty(imagePath))
                {
                    ImageHelper.DeleteImage(imagePath);
                }
            }


            string folder = "uploads/projeler";
            int targetWidth = 391;
            int targetHeight = 220;

            var result = await ImageHelper.ResizeAndUploadImage(folder,projectUpdateDto.FormFileCoverImageUrl,targetWidth,targetHeight,_httpContextAccessor.HttpContext.Request);

            projectUpdateDto.siteBaseUrl = BaseUrl;
            projectUpdateDto.CoverImageUrl = result;
        }

        private async Task HandleGalleryUpdate(ProjectUpdateDto projectUpdateDto)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string BaseUrl = $"{request.Scheme}://{request.Host.Value}/";

            string folder = "uploads/projeler/gallery";
            int targetWidth = 390;
            int targetHeight = 420;

            foreach (var file in projectUpdateDto.GalleryFiles)
            {
                var result = await ImageHelper.ResizeAndUploadImage(
                    folder,
                    file,
                    targetWidth,
                    targetHeight,
                    _httpContextAccessor.HttpContext.Request
                );

                var galleryItem = new ProjectImagesAddDto()
                {
                    Name = file.FileName,
                    URL = result,
                    siteBaseUrl = BaseUrl,
                    ProjectId = projectUpdateDto.Id
                };

                await _projectImagesService.Add(galleryItem);
            }
        }

         

        [HttpPost]
        public async Task<IActionResult> GetProjectCategory(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var projectCategory = await _projectCategoryService.Get(id);

            if (projectCategory != null)
            {
                var categoryName = projectCategory.Data.ProjectCategory.Name;
                return new JsonResult(new { success = true, categoryName });
            }

            return Json(new { success = false, message = "Project category not found." });
        }

        [HttpPost]
        public async Task<IActionResult> DoHardDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var service = await _projectService.Get(id);
            await _projectService.HardDelete(id);

            if (service != null && service.ResultStatus == ResultStatus.Success && service.Data?.Project != null)
            {
                var imagePath = service.Data.Project.CoverImageUrl;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    ImageHelper.DeleteImage(imagePath);
                }
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> ProjectImagesDelete(int id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var multiImages = await _projectImagesService.Get(id);
            await _projectImagesService.HardDelete(id);

            //if (multiImages != null)
            //{
            //    var imagePath = multiImages.Data.ProjectImage.URL;
            //    if (!string.IsNullOrEmpty(imagePath))
            //    {
            //        ImageHelper.DeleteImage(imagePath);
            //    }
            //}


            return Json(new { success = true });
        }

    }
}
