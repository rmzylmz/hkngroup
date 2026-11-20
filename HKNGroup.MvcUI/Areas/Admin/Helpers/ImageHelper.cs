using SkiaSharp;

namespace HKNGroup.WebUI.Areas.Admin.Helpers
{
    public static class ImageHelper
    {
        public static async Task<string> ResizeAndUploadImage(string folderPath, IFormFile file, int targetWidth, int targetHeight, HttpRequest request)
        {
            if (file == null || file.Length == 0)
            {
                return string.Empty;
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderPath);
            string coverFolder = Path.Combine(uploadsFolder, "cover");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            if (!Directory.Exists(coverFolder))
            {
                Directory.CreateDirectory(coverFolder);
            }

            // Dosya adı: Daima standart ve güvenli .jpg uzantısı ile üretilir
            string uniqueFileName = $"{DateTime.Now:yyyyMMddHHmmssfff}_{Guid.NewGuid().ToString("N")[..8]}.jpg";
            string originalFilePath = Path.Combine(uploadsFolder, uniqueFileName);
            string resizedFilePath = Path.Combine(coverFolder, uniqueFileName);

            try
            {
                using var source = file.OpenReadStream();
                using var codec = SKCodec.Create(source);
                if (codec is null)
                {
                    throw new InvalidDataException("Geçerli veya desteklenen bir görsel formatı değil. Lütfen JPG, JPEG, PNG veya WEBP formatında geçerli bir resim yükleyiniz.");
                }

                using var image = new SKBitmap(codec.Info.WithColorType(SKColorType.Rgba8888));
                if (codec.GetPixels(image.Info, image.GetPixels()) != SKCodecResult.Success)
                {
                    throw new InvalidDataException("Görsel dosyasının içeriği hasarlı veya bozuk.");
                }

                using var cover = image.Resize(
                    image.Info.WithSize(targetWidth, targetHeight),
                    new SKSamplingOptions(SKCubicResampler.Mitchell))
                    ?? throw new InvalidOperationException("Görsel hedef boyuta ölçeklenemedi.");

                using var originalJpeg = EncodeJpeg(image);
                using var coverJpeg = EncodeJpeg(cover);
                await File.WriteAllBytesAsync(originalFilePath, originalJpeg.ToArray());
                await File.WriteAllBytesAsync(resizedFilePath, coverJpeg.ToArray());

                return $"{folderPath}/cover/{uniqueFileName}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Görsel işlenirken hata oluştu ({file.FileName}): {ex.Message}", ex);
            }
        }

        private static SKData EncodeJpeg(SKBitmap bitmap)
        {
            using var image = SKImage.FromBitmap(bitmap);
            return image.Encode(SKEncodedImageFormat.Jpeg, 85)
                ?? throw new InvalidOperationException("Görsel JPEG formatına dönüştürülemedi.");
        }

        public static void DeleteImage(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath))
                return;

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            string fullPath = Path.Combine(uploadsFolder, imagePath.TrimStart('/', '\\'));

            if (File.Exists(fullPath))
            {
                try { File.Delete(fullPath); } catch { }
            }

            try
            {
                string mainFolder = Path.GetDirectoryName(imagePath) ?? "";
                string mainFolderPath = Path.GetDirectoryName(mainFolder) ?? "";
                string mainFileName = Path.GetFileName(imagePath);
                string mainFullPath = Path.Combine(uploadsFolder, mainFolderPath, mainFileName);

                if (File.Exists(mainFullPath))
                {
                    File.Delete(mainFullPath);
                }
            }
            catch
            {
                // Silme işlemi başarısız olsa dahi akışı bozma
            }
        }
    }
}
