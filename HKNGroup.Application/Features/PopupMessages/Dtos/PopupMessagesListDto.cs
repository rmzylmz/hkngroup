namespace HKNGroup.Application.Features.PopupMessages
{
    public class PopupMessagesListDto : DtoGetBase
    {
        public IList<HKNGroup.Domain.Entities.PopupMessages>? PopupMessages { get; set; } 
    }
}
