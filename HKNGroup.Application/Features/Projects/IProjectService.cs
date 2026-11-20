
namespace HKNGroup.Application.Features.Projects
{
    public interface IProjectService
    {
        Task<IDataResult<ProjectDto>> Get(int projectId);
        Task<IDataResult<ProjectDto>> GetString(string name);
        Task<IDataResult<ProjectUpdateDto>> GetUpdateDto(int projectId);
        Task<IDataResult<ProjectListDto>> GetAll();
        Task<IDataResult<ProjectListDto>> GetAllByNonDelete();
        Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActiveWithProjectCategoryId(int categoryId);
        Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActiveOrderByDescendingId();
        Task<IDataResult<ProjectListDto>> Get3MostReadByNonDeleteAndActive();
        Task<IDataResult<ProjectDto>> Add(ProjectAddDto projectAddDto);
        Task<IDataResult<ProjectDto>> Update(ProjectUpdateDto projectUpdateDto);
        Task<IResult> AddViews(int projectId);
        Task<IResult> Delete(int projectId);
        Task<IResult> HardDelete(int projectId);
    }
}

