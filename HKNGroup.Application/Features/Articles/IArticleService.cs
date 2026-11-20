
namespace HKNGroup.Application.Features.Articles
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);
        Task<IDataResult<ArticleUpdateDto>> GetUpdateDto(int articleId);
        Task<IDataResult<ArticleListDto>> GetAll();
        Task<IDataResult<ArticleDto>> GetString(string name);
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleteAndActiveWithCategoryId(int categoryId);
        Task<IDataResult<ArticleListDto>> GetAllByNonDelete();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleteAndActiveSite();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeleteAndActiveOrderByDescendingId();
        Task<IDataResult<ArticleListDto>> Get3MostReadByNonDeleteAndActive();
        Task<IDataResult<ArticleDto>> Add(ArticleAddDto articleAddDto, string createdByName);
        Task<IDataResult<ArticleDto>> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> AddViews(int articleId);
        Task<IResult> Delete(int articleId, string modifiedByName);
        Task<IResult> HardDelete(int articleId);
    }
}

