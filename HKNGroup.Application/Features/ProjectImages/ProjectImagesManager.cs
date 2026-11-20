using ProjectImages = HKNGroup.Domain.Entities.ProjectImages;
using AutoMapper;

namespace HKNGroup.Application.Features.ProjectImages
{
    public class ProjectImagesManager : IProjectImagesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectImagesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ProjectImagesDto>> Add(ProjectImagesAddDto projectImagesAddDto)
        {
            var projectImages = _mapper.Map<HKNGroup.Domain.Entities.ProjectImages>(projectImagesAddDto);
            projectImages.ModifiedTime = DateTime.Now; 
            var addedprojectImages = await _unitOfWork.ProjectImages.AddAsync(projectImages);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectImagesDto>(ResultStatus.Success, $"{addedprojectImages.Name} isimli ürün başarılı bir şekilde kayıt edilmiştir.", new ProjectImagesDto
            {
                ProjectImage = addedprojectImages,
                Message = $"{addedprojectImages.Name} isimli ürün başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IDataResult<ProjectImagesDto>> Get(int imageId)
        {
            var images = await _unitOfWork.ProjectImages.GetAsync(x => x.Id == imageId);
             if (images != null)
            {
                return new DataResult<ProjectImagesDto>(ResultStatus.Success, new ProjectImagesDto
                {
                    ProjectImage = images,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectImagesDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ProjectImagesDto
            {
                ProjectImage = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!"
            });
        }


        public async Task<IDataResult<ProjectImagesListDto>> GetAllProjectImagesId(int projectId)
        {
            var projects = await _unitOfWork.ProjectImages.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true && x.ProjectId == projectId);

            if (projects != null)
            {
                return new DataResult<ProjectImagesListDto>(ResultStatus.Success, new ProjectImagesListDto
                {
                    ProjectImages = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectImagesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectImagesListDto
            {
                ProjectImages = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }
        public async Task<IResult> HardDelete(int projectId)
        {
            var project = await _unitOfWork.ProjectImages.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                await _unitOfWork.ProjectImages.DeleteAsync(project);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{project.Name} başlıklı makale başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }
    }
}



