
namespace HKNGroup.Application.Features.Sliders
{
    public interface ISliderService
    {
        Task<IDataResult<SliderDto>> Get(int homePageSliderId);
        Task<IDataResult<SliderUpdateDto>> GetUpdateDto(int homePageSliderId);
        Task<IDataResult<SliderListDto>> GetAll();
        Task<IDataResult<SliderListDto>> GetAllByNonDelete();
        Task<IDataResult<SliderListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<SliderDto>> Add(SliderAddDto homePageSliderAddDto, string createdByName);
        Task<IDataResult<SliderDto>> Update(SliderUpdateDto homePageSliderUpdateDto, string modifiedByName);
        Task<IResult> Delete(int homePageSliderId, string modifiedByName);
        Task<IResult> HardDelete(int homePageSliderId);
    }
}

