
namespace HKNGroup.Application.Features.ArticleComments
{
    public class ArticleCommentListDto : DtoGetBase
    {
        public IList<ArticleComment>? Comments { get; set; }
    }
}

