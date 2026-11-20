
namespace HKNGroup.Application.Features.ArticleCategories
{
    public class ArticleCategoryListDto : DtoGetBase
    {
        public IList<ArticleCategory>? Categories { get; set; }
    }
}

