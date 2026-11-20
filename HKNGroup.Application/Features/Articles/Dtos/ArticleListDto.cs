
namespace HKNGroup.Application.Features.Articles
{
    public class ArticleListDto : DtoGetBase
    {
        public IList<Article>? Articles { get; set; }
    }
}

