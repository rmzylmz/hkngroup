using Microsoft.AspNetCore.Mvc;

namespace HKNGroup.WebUI.ViewComponents
{
    public class FooterAdress : ViewComponent
    {
       
        public IViewComponentResult Invoke()
        {
            return View();
        }
       
    }
}
