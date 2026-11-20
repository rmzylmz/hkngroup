
namespace HKNGroup.Application.Features.ArticleComments
{
    public interface IArticleCommentService
    {
        Task<IDataResult<ArticleCommentDto>> Get(int commentId);
        Task<IDataResult<ArticleCommentUpdateDto>> GetUpdateDto(int commentId);
        Task<IDataResult<ArticleCommentListDto>> GetAll();
        Task<IDataResult<ArticleCommentListDto>> GetAllByNonDelete();
        Task<IDataResult<ArticleCommentListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ArticleCommentListDto>> GetAllByNonDeleteAndActiveSite();
        Task<IDataResult<ArticleCommentDto>> Add(ArticleCommentAddDto commentAddDto, string createdByName);
        Task<IDataResult<ArticleCommentDto>> Update(ArticleCommentUpdateDto commentUpdateDto, string modifiedByName);
        Task<IResult> DoActive(int commentId, string modifiedByName);
        Task<IResult> Delete(int commentId, string modifiedByName);
        Task<IResult> HardDelete(int commentId);
    }
}

