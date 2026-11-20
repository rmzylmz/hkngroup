using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class PopupMessage : ViewComponent
    {
        private readonly IPopupMessagesService _popupMessagesService;

        public PopupMessage(IPopupMessagesService popupMessagesService)
        {
            _popupMessagesService = popupMessagesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var popupMessages = await _popupMessagesService.GetAllByNonDeleteAndActive();
            return View(popupMessages?.Data);
        }
    }
}
