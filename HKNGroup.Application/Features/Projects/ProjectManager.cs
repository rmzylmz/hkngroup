using AutoMapper;

namespace HKNGroup.Application.Features.Projects
{
    public class ProjectManager : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ProjectDto>> Add(ProjectAddDto projectAddDto)
        {
            var project = _mapper.Map<Project>(projectAddDto);
            project.ModifiedTime = DateTime.Now;
            project.Url = SeoHelper.ToSeoUrl(projectAddDto.Title);
            project.ProjectImages = new List<HKNGroup.Domain.Entities.ProjectImages>();

            if (projectAddDto.Gallery != null)
            {
                foreach (var file in projectAddDto.Gallery)
                {
                    project.ProjectImages.Add(new HKNGroup.Domain.Entities.ProjectImages()
                    {
                        Name = file.Name,
                        URL = file.URL,
                        siteBaseUrl = file.siteBaseUrl
                    });
                }
            }

            var added = await _unitOfWork.Project.AddAsync(project);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectDto>(ResultStatus.Success, $"{added.Title} isimli proje başarılı bir şekilde kayıt edilmiştir.", new ProjectDto
            {
                Project = added,
                Message = $"{added.Title} isimli proje başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> AddViews(int projectId)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                project.ViewsCount = project.ViewsCount + 1;
                await _unitOfWork.Project.UpdateAsync(project);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success);
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IResult> Delete(int projectId)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                project.IsDeleted = true;
                project.ModifiedTime = DateTime.Now;
                await _unitOfWork.Project.UpdateAsync(project);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{project.Title} başlıklı proje başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ProjectDto>> Get(int projectId)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                return new DataResult<ProjectDto>(ResultStatus.Success, new ProjectDto
                {
                    Project = project,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ProjectDto
            {
                Project = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectDto>> GetString(string name)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Url == name);
            if (project != null)
            {
                return new DataResult<ProjectDto>(ResultStatus.Success, new ProjectDto
                {
                    Project = project,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ProjectDto
            {
                Project = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> Get3MostReadByNonDeleteAndActive()
        {
            var projects = await _unitOfWork.Project.Get3MostReadAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAll()
        {
            var projects = await _unitOfWork.Project.GetAllAsync();
        
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDelete()
        {
            var projects = await _unitOfWork.Project.GetAllAsync(x => x.IsDeleted == false);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActive()
        {
            var projects = await _unitOfWork.Project.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true, x => x.Category);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActiveOrderByDescendingId()
        {
            var projects = await _unitOfWork.Project.GetAllOrderByDescIdAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActiveWithProjectCategoryId(int categoryId)
        {
            var projects = await _unitOfWork.Project.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true && x.ProjectCategoryId == categoryId);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectListDto>> GetAllByNonDeleteAndActiveWithProjectCount()
        {
            var projects = await _unitOfWork.Project.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (projects != null)
            {
                return new DataResult<ProjectListDto>(ResultStatus.Success, new ProjectListDto
                {
                    Projects = projects,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectListDto
            {
                Projects = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectUpdateDto>> GetUpdateDto(int projectId)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                var projectUpdateDto = _mapper.Map<ProjectUpdateDto>(project);
                return new DataResult<ProjectUpdateDto>(ResultStatus.Success, projectUpdateDto);
            }
            return new DataResult<ProjectUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int projectId)
        {
            var project = await _unitOfWork.Project.GetAsync(x => x.Id == projectId);
            if (project != null)
            {
                await _unitOfWork.Project.DeleteAsync(project);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{project.Title} başlıklı proje başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ProjectDto>> Update(ProjectUpdateDto projectUpdateDto)
        {
            var project = _mapper.Map<Project>(projectUpdateDto);
            project.Url = SeoHelper.ToSeoUrl(projectUpdateDto.Title);
            var updatedProject = await _unitOfWork.Project.UpdateAsync(project);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectDto>(ResultStatus.Success, new ProjectDto
            {
                Project = updatedProject,
                ResultStatus = ResultStatus.Success
            });
        }
    }
}



