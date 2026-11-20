using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISiteSettingsService _siteIdentityService;
        private readonly IMessageService _messageService;

        public ContactController(ISiteSettingsService siteIdentityService, IMessageService messageService)
        {
            _siteIdentityService = siteIdentityService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.Contact = "active";
            ViewData["Breadcumb"] = "İletişim";
            var identity = await _siteIdentityService.GetUpdateDto(1); 
            if (identity.ResultStatus == ResultStatus.Success)
            {
                ViewBag.SeoTitle = identity.Data.SiteDefaultTitle;
                ViewBag.SeoDescription = identity.Data.SiteDescription;
                ViewBag.Keywords = identity.Data.SiteKeyword;

                return View(identity.Data);
            }
            if (identity.ResultStatus == ResultStatus.Error)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string name, string email, string number, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Lütfen gerekli alanları doldurunuz.");
            }

            var trimmedName = name.Trim();
            var nameParts = trimmedName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.Length > 0 ? nameParts[0] : trimmedName;
            var lastName = nameParts.Length > 1 ? string.Join(" ", nameParts.Skip(1)) : "-";

            var fullText = !string.IsNullOrWhiteSpace(number)
                ? $"[Tel: {number.Trim()}] {message.Trim()}"
                : message.Trim();

            if (fullText.Length > 512)
            {
                fullText = fullText.Substring(0, 512);
            }

            var subjectText = !string.IsNullOrWhiteSpace(subject) ? subject.Trim() : "İletişim Formu";
            if (subjectText.Length > 50)
            {
                subjectText = subjectText.Substring(0, 50);
            }

            var messageAddDto = new MessageAddDto
            {
                FirstName = firstName.Length > 25 ? firstName.Substring(0, 25) : firstName,
                LastName = lastName.Length > 25 ? lastName.Substring(0, 25) : lastName,
                Email = email.Trim().Length > 100 ? email.Trim().Substring(0, 100) : email.Trim(),
                Subject = subjectText,
                Text = fullText
            };

            var result = await _messageService.Add(messageAddDto, $"{firstName} {lastName}");
            if (result.ResultStatus == ResultStatus.Success)
            {
                return Ok("Mesajınız başarıyla iletildi. En kısa sürede sizinle iletişime geçilecektir.");
            }

            return BadRequest(result.Message ?? "Mesaj kaydedilirken bir hata oluştu.");
        }
    }
}
