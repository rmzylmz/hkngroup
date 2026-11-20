
namespace HKNGroup.WebUI.Models
{
    public class ProjectViewModel
    {
        public Project ProjectDetay { get; set; }
        public IList<ProjectImages> ProjectImages { get; set; }
        public IList<Project> ProjectList { get; set; }
        public IList<ProjectCategory> ProjectCategories { get; set; }
        public SiteSettingsUpdateDto SiteSetting { get; set; }    
    }
}
