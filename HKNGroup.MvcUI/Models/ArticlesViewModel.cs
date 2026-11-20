
namespace HKNGroup.WebUI.Models
{
    public class ArticlesViewModel
    {
        public IList<Article> Articles { get; set; }
        public Article ArticleDetay { get; set; }
        public SiteSettingsUpdateDto SiteSettings { get; set; }    
    }
}
