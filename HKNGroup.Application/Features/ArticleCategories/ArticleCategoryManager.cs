using AutoMapper;

namespace HKNGroup.Application.Features.ArticleCategories
{
    public class ArticleCategoryManager : IArticleCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleCategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<ArticleCategoryDto>> Add(ArticleCategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<ArticleCategory>(categoryAddDto);
            category.ModifiedTime = DateTime.Now;
            category.Url = SeoHelper.ToSeoUrl(categoryAddDto.Name);
            var addedCategory = await _unitOfWork.ArticleCategory.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleCategoryDto>(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde kayıt edilmiştir.", new ArticleCategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{category.Name} isimli kategori başarılı bir şekilde kayıt edilmiştir."
            });
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.ArticleCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedTime = DateTime.Now;
                await _unitOfWork.ArticleCategory.UpdateAsync(category);
                return new Result(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ArticleCategoryDto>> Get(int categoryId)
        {
            var category = await _unitOfWork.ArticleCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                return new DataResult<ArticleCategoryDto>(ResultStatus.Success, new ArticleCategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCategoryDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ArticleCategoryDto
            {
                ResultStatus = ResultStatus.Error,
                Category = null,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.ArticleCategory.GetAllAsync();
            if (categories != null)
            {
                return new DataResult<ArticleCategoryListDto>(ResultStatus.Success, new ArticleCategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,

                });
            }
            return new DataResult<ArticleCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDelete()
        {
            var categories = await _unitOfWork.ArticleCategory.GetAllAsync(x => x.IsDeleted == false);
            if (categories != null)
            {
                return new DataResult<ArticleCategoryListDto>(ResultStatus.Success, new ArticleCategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success,

                });
            }
            return new DataResult<ArticleCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActive()
        {
            var categories = await _unitOfWork.ArticleCategory.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (categories != null)
            {
                return new DataResult<ArticleCategoryListDto>(ResultStatus.Success, new ArticleCategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActiveSite()
        {
            var categories = await _unitOfWork.ArticleCategory.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (categories != null)
            {
                return new DataResult<ArticleCategoryListDto>(ResultStatus.Success, new ArticleCategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCategoryListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCategoryUpdateDto>> GetUpdateDto(int categoryId)
        {
            var category = await _unitOfWork.ArticleCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                var categoryUpdateDto = _mapper.Map<ArticleCategoryUpdateDto>(category);
                return new DataResult<ArticleCategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            return new DataResult<ArticleCategoryUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.ArticleCategory.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.ArticleCategory.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{category.Name} isimli kategori başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ArticleCategoryDto>> Update(ArticleCategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<ArticleCategory>(categoryUpdateDto);
            category.Url = SeoHelper.ToSeoUrl(categoryUpdateDto.Name);

            var updatedCategory = await _unitOfWork.ArticleCategory.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleCategoryDto>(ResultStatus.Success, new ArticleCategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success
            });
        }
      
        public async Task<IDataResult<ArticleCategoryDto>> GetString(string name)
        {
            var category = await _unitOfWork.ArticleCategory.GetAsync(x => x.Url == name);
            if (category != null)
            {
                return new DataResult<ArticleCategoryDto>(ResultStatus.Success, new ArticleCategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCategoryDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ArticleCategoryDto
            {
                ResultStatus = ResultStatus.Error,
                Category = null,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public Task<IDataResult<ArticleCategoryListDto>> GetAllByNonDeleteAndActiveWithCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}

