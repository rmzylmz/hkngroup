
namespace HKNGroup.Application.Features.References
{
    public interface IReferanceService
    {
        Task<IDataResult<ReferanceDto>> Get(int referanceId);
        Task<IDataResult<ReferanceUpdateDto>> GetUpdateDto(int referanceId);
        Task<IDataResult<ReferanceListDto>> GetAll();
        Task<IDataResult<ReferanceListDto>> GetAllByNonDelete();
        Task<IDataResult<ReferanceListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ReferanceDto>> Add(ReferanceAddDto referanceAddDto, string createdByName);
        Task<IDataResult<ReferanceDto>> Update(ReferanceUpdateDto referanceUpdateDto, string modifiedByName);
        Task<IResult> Delete(int referanceId, string modifiedByName);
        Task<IResult> HardDelete(int referanceId);
    }
}

