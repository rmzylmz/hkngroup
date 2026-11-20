
namespace HKNGroup.Application.Features.ProjectCategories
{
    public interface IProjectCategoryService
    {
        Task<IDataResult<ProjectCategoryDto>> Get(int categoryId);
        Task<IDataResult<ProjectCategoryDto>> GeString(string name);
       
        Task<IDataResult<ProjectCategoryUpdateDto>> GetUpdateDto(int categoryId);
        Task<IDataResult<ProjectCategoryListDto>> GetAll();
        Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDelete();
        Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDeleteAndActive();
        Task<IDataResult<ProjectCategoryDto>> Add(ProjectCategoryAddDto categoryAddDto);
        Task<IDataResult<ProjectCategoryDto>> Update(ProjectCategoryUpdateDto categoryUpdateDto);
        Task<IResult> Delete(int categoryId);
        Task<IResult> HardDelete(int categoryId);
    }
}

