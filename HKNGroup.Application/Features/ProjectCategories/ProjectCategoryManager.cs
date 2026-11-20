using AutoMapper;

namespace HKNGroup.Application.Features.ProjectCategories
{
    public class ProjectCategoryManager : IProjectCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<ProjectCategoryDto>> Add(ProjectCategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<ProjectCategory>(categoryAddDto);
            category.ModifiedTime = DateTime.Now;
            category.Url = SeoHelper.ToSeoUrl(categoryAddDto.Name);
            var addedProjectCategory = await _unitOfWork.ProjectCategory.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectCategoryDto>(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde kayıt edilmiştir.", new ProjectCategoryDto
            {
                ProjectCategory = addedProjectCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{category.Name} isimli kategori başarılı bir şekilde kayıt edilmiştir."
            });
        }

        public async Task<IResult> Delete(int categoryId)
        {
            var category = await _unitOfWork.ProjectCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedTime = DateTime.Now;
                await _unitOfWork.ProjectCategory.UpdateAsync(category);
                return new Result(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ProjectCategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.ProjectCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                return new DataResult<ProjectCategoryDto>(ResultStatus.Success, new ProjectCategoryDto
                {
                    ProjectCategory = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectCategoryDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ProjectCategoryDto
            {
                ResultStatus = ResultStatus.Error,
                ProjectCategory = null,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectCategoryDto>> GeString(string name)
        {
            var category = await _unitOfWork.ProjectCategory.GetAsync(x => x.Url == name);
            if (category != null)
            {
                return new DataResult<ProjectCategoryDto>(ResultStatus.Success, new ProjectCategoryDto
                {
                    ProjectCategory = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectCategoryDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ProjectCategoryDto
            {
                ResultStatus = ResultStatus.Error,
                ProjectCategory = null,
                Message = "Hata, kayıt bulunamadı!"
            });
        }
        public async Task<IDataResult<ProjectCategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.ProjectCategory.GetAllAsync();
            if (categories != null)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Success, new ProjectCategoryListDto
                {
                    ProjectCategories = categories,
                    ResultStatus = ResultStatus.Success,

                });
            }
            return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectCategoryListDto
            {
                ProjectCategories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.ProjectCategory.GetAllAsync(x => x.IsDeleted == false);
            if (categories != null)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Success, new ProjectCategoryListDto
                {
                    ProjectCategories = categories,
                    ResultStatus = ResultStatus.Success,

                });
            }
            return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectCategoryListDto
            {
                ProjectCategories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectCategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var categories = await _unitOfWork.ProjectCategory.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (categories != null)
            {
                return new DataResult<ProjectCategoryListDto>(ResultStatus.Success, new ProjectCategoryListDto
                {
                    ProjectCategories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ProjectCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ProjectCategoryListDto
            {
                ProjectCategories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ProjectCategoryUpdateDto>> GetUpdateDto(int categoryId)
        {
            var category = await _unitOfWork.ProjectCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                var categoryUpdateDto = _mapper.Map<ProjectCategoryUpdateDto>(category);
                return new DataResult<ProjectCategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<ProjectCategoryUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.ProjectCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.ProjectCategory.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ProjectCategoryDto>> Update(ProjectCategoryUpdateDto categoryUpdateDto)
        {
            var category = _mapper.Map<ProjectCategory>(categoryUpdateDto);

            category.Url = SeoHelper.ToSeoUrl(categoryUpdateDto.Name);
            var updatedProjectCategory = await _unitOfWork.ProjectCategory.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<ProjectCategoryDto>(ResultStatus.Success, new ProjectCategoryDto
            {
                ProjectCategory = updatedProjectCategory,
                ResultStatus = ResultStatus.Success
            });
        }


    }
}

