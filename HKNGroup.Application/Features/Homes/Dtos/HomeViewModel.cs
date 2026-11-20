using HKNGroup.Application.Features.SiteSettings;

namespace HKNGroup.Application.Features.Homes
{
    public class HomeViewModel
    {
        public IList<Article>? Articles { get; set; }
        public IList<Referance>? Referances { get; set; }
        public IList<Project>? Projects { get; set; }
        public SiteSettingsUpdateDto? SiteSetting { get; set; }
    }
}
