
namespace HKNGroup.WebUI.Models
{
    public class HomeViewModel
    {
        public IList<Slider> Sliders { get; set; }
        public IList<Page> Pages { get; set; }
        public SiteSettingsUpdateDto SiteSetting { get; set; }    
    }
}
