
namespace HKNGroup.WebUI.Models
{
    public class OurServicesViewModel
    {
        public OurService OurServiceDetay { get; set; }
        public IList<OurService> OurServiceList { get; set; }
        public SiteSettingsUpdateDto SiteSetting { get; set; }    
    }
}
