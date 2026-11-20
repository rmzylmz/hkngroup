using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UploadImageController : Controller
    {
        private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".webp", ".gif"
        };

        private const long MaxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB

        [HttpPost]
        public async Task<JsonResult> UploadCKEditorImage()
        {
            var files = Request.Form.Files;
            if (files.Count == 0)
            {
                return Json(new { uploaded = false, url = string.Empty, error = new { message = "Dosya seçilmedi." } });
            }

            var formFile = files[0];
            var ext = Path.GetExtension(formFile.FileName)?.ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            {
                return Json(new { uploaded = false, url = string.Empty, error = new { message = "Geçersiz dosya türü. Sadece görsel dosyaları (.jpg, .jpeg, .png, .webp, .gif) yüklenebilir." } });
            }

            if (formFile.Length > MaxFileSizeInBytes)
            {
                return Json(new { uploaded = false, url = string.Empty, error = new { message = "Dosya boyutu 5 MB'den büyük olamaz." } });
            }

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var saveDir = Path.Combine(webRoot, "uploads", "img");
            var savePath = Path.Combine(saveDir, fileName);
            var previewPath = $"/uploads/img/{fileName}";

            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }

                await using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await formFile.CopyToAsync(fs);
                }

                return Json(new { uploaded = true, url = previewPath });
            }
            catch (Exception)
            {
                return Json(new { uploaded = false, url = string.Empty, error = new { message = "Dosya yüklenirken bir hata oluştu." } });
            }
        }

        [HttpPost]
        public async Task<JsonResult> UploadTrombovImage()
        {
            var image = Request.Form.Files;
            if (image.Count == 0)
            {
                return Json(new { success = false, url = string.Empty, message = "Dosya seçilmedi." });
            }

            var formFile = image[0];
            var ext = Path.GetExtension(formFile.FileName)?.ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
            {
                return Json(new { success = false, url = string.Empty, message = "Geçersiz dosya türü." });
            }

            if (formFile.Length > MaxFileSizeInBytes)
            {
                return Json(new { success = false, url = string.Empty, message = "Dosya boyutu 5 MB'den büyük olamaz." });
            }

            var fileName = $"{Guid.NewGuid():N}{ext}";
            var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var saveDir = Path.Combine(webRoot, "uploads", "tImg");
            var savePath = Path.Combine(saveDir, fileName);
            var previewPath = $"/uploads/tImg/{fileName}";

            try
            {
                if (!Directory.Exists(saveDir))
                {
                    Directory.CreateDirectory(saveDir);
                }

                await using (var fs = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await formFile.CopyToAsync(fs);
                }

                return Json(new { success = true, url = previewPath });
            }
            catch (Exception)
            {
                return Json(new { success = false, url = string.Empty, message = "Görsel yüklenirken bir hata oluştu." });
            }
        }
    }
}
