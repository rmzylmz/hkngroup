
namespace HKNGroup.Application.Features.ArticleCategories
{
    public interface IArticleCategoryService
    {
        Task<IDataResult<ArticleCategoryDto>> Get(int categoryId);
        Task<IDataResult<ArticleCategoryUpdateDto>> GetUpdateDto(int categoryId);
        Task<IDataResult<ArticleCategoryDto>> GetString(string name);
        Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActiveWithCategoryId(int categoryId);
        Task<IDataResult<ArticleCategoryListDto>> GetAll();
        Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDelete();
        Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActiveSite();
        Task<IDataResult<ArticleCategoryDto>> Add(ArticleCategoryAddDto categoryAddDto, string createdByName);
        Task<IDataResult<ArticleCategoryDto>> Update(ArticleCategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);
        Task<IResult> HardDelete(int categoryId);
    }
}

