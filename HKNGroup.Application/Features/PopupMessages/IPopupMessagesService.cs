
namespace HKNGroup.Application.Features.PopupMessages
{
    public interface IPopupMessagesService
    {
        Task<IDataResult<PopupMessagesDto>> Get(int popupMessagesId);
        Task<IDataResult<PopupMessagesUpdateDto>> GetUpdateDto(int popupMessagesId);
        Task<IDataResult<PopupMessagesListDto>> GetAll();
        Task<IDataResult<PopupMessagesListDto>> GetAllByNonDelete();
        Task<IDataResult<PopupMessagesListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<PopupMessagesDto>> Add(PopupMessagesAddDto popupMessagesAddDto, string createdByName);
        Task<IDataResult<PopupMessagesDto>> Update(PopupMessagesUpdateDto popupMessagesUpdateDto, string modifiedByName);
        Task<IResult> Delete(int popupMessagesId, string modifiedByName);
        Task<IResult> HardDelete(int popupMessagesId);
    }
}

