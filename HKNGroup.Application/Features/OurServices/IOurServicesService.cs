
namespace HKNGroup.Application.Features.OurServices
{
    public interface IOurServicesService
    {
        Task<IDataResult<OurServicesDto>> Get(int ourServicesId);
        Task<IDataResult<OurServicesDto>> GetString(string name);
        Task<IDataResult<OurServicesUpdateDto>> GetUpdateDto(int ourServicesId);
        Task<IDataResult<OurServicesListDto>> GetAll();
        Task<IDataResult<OurServicesListDto>> GetAllByNonDelete();
        Task<IDataResult<OurServicesListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<OurServicesDto>> Add(OurServicesAddDto ourServicesAddDto);
        Task<IDataResult<OurServicesDto>> Update(OurServicesUpdateDto ourServicesUpdateDto);
        Task<IResult> Delete(int ourServicesId);
        Task<IResult> HardDelete(int ourServicesId);
    }
}

