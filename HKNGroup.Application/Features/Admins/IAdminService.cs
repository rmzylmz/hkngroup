
namespace HKNGroup.Application.Features.Admins
{
    public interface IAdminService
    {
        Task<IDataResult<AdminDto>> Get(int adminId);
        Task<IDataResult<AdminDto>> GetByUserName(string userName);
        Task<IDataResult<AdminDto>> Authenticate(string userName, string password);
        Task<IDataResult<AdminUpdateDto>> GetUpdateDto(int adminId);
        Task<IDataResult<AdminDto>> Update(AdminUpdateDto adminUpdateDto, string modifiedByName);
        Task<IResult> Delete(int adminId, string modifiedByName);
        Task<IResult> HardDelete(int adminId);
    }
}

