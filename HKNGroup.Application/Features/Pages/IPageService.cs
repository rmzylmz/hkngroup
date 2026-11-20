

namespace HKNGroup.Application.Features.Pages
{
    public interface IPageService
    {
        Task<IDataResult<PageDto>> Get(int pageId);
        Task<IDataResult<PageDto>> GetString(string pageName);
        Task<IDataResult<PageUpdateDto>> GetUpdateDto(int pageId);
        Task<IDataResult<PageListDto>> GetAll(); 
        Task<IDataResult<PageListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<PageListDto>> GetAllByNonDeleteAndActiveAndIsMainPage();
        Task<IDataResult<PageDto>> Add(PageAddDto pageAddDto, string createdByName);
        Task<IDataResult<PageDto>> Update(PageUpdateDto pageUpdateDto, string modifiedByName);
        Task<IResult> Delete(int pageId, string modifiedByName);
        Task<IResult> HardDelete(int pageId);
        Task<IResult> AddViews(int articleId);

    }
}

