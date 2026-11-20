using ProjectImages = HKNGroup.Domain.Entities.ProjectImages;

namespace HKNGroup.Application.Features.ProjectImages
{
    public interface IProjectImagesService
    {
        Task<IDataResult<ProjectImagesDto>> Add(ProjectImagesAddDto projectImagesAddDto);
        Task<IDataResult<ProjectImagesListDto>> GetAllProjectImagesId(int projectId);
        Task<IDataResult<ProjectImagesDto>> Get(int imageId);

        Task<IResult> HardDelete(int projectId);
    }
}


