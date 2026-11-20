using AutoMapper;

namespace HKNGroup.Application.Features.Pages
{
    public class PageManager : IPageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PageManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IResult> AddViews(int pageId)
        {
            var article = await _unitOfWork.Page.GetAsync(x => x.Id == pageId);
            if (article != null)
            {

                await _unitOfWork.Page.UpdateAsync(article);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success);
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }
        public async Task<IDataResult<PageDto>> Add(PageAddDto pageAddDto, string createdByName)
        {
            var page = _mapper.Map<Page>(pageAddDto);
            page.ModifiedTime = DateTime.Now;

            var addedPage = await _unitOfWork.Page.AddAsync(page);
            await _unitOfWork.SaveAsync();
            return new DataResult<PageDto>(ResultStatus.Success, $"{addedPage.PageTitle} isimli sayfa başarılı bir şekilde kayıt edilmiştir.", new PageDto
            {
                Page = addedPage,
                Message = $"{addedPage.PageTitle} isimli sayfa başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int pageId, string modifiedByName)
        {
            var page = await _unitOfWork.Page.GetAsync(x => x.Id == pageId);
            if (page != null)
            {
                page.IsDeleted = true;
                await _unitOfWork.Page.UpdateAsync(page);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{page.PageTitle} isimli sayfa başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<PageDto>> Get(int pageId)
        {
            var page = await _unitOfWork.Page.GetAsync(x => x.PageNumber == pageId);
            if (page != null)
            {
                return new DataResult<PageDto>(ResultStatus.Success, new PageDto
                {
                    Page = page,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<PageDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new PageDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                Page = null
            });
        }

        public async Task<IDataResult<PageDto>> GetString(string pageName)
        {
            var page = await _unitOfWork.Page.GetAsync(x => x.Url == pageName);
            if (page != null)
            {
                return new DataResult<PageDto>(ResultStatus.Success, new PageDto
                {
                    Page = page,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<PageDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new PageDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                Page = null
            });
        }

        public async Task<IDataResult<PageListDto>> GetAll()
        {
            var pages = await _unitOfWork.Page.GetAllAsync();
            if (pages != null)
            {
                return new DataResult<PageListDto>(ResultStatus.Success, new PageListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Page = pages
                });
            }
            return new DataResult<PageListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PageListDto
            {
                ResultStatus = ResultStatus.Error,
                Page = pages,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

 
        public async Task<IDataResult<PageListDto>> GetAllByNonDeleteAndActive()
        {
            var pages = await _unitOfWork.Page.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);

            if (pages != null)
            {
                return new DataResult<PageListDto>(ResultStatus.Success, new PageListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Page = pages
                });
            }
            return new DataResult<PageListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PageListDto
            {
                ResultStatus = ResultStatus.Error,
                Page = pages,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<PageListDto>> GetAllByNonDeleteAndActiveAndIsMainPage()
        {
            var pages = await _unitOfWork.Page.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true && x.IsMainPage==true);

            if (pages != null)
            {
                return new DataResult<PageListDto>(ResultStatus.Success, new PageListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Page = pages
                });
            }
            return new DataResult<PageListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PageListDto
            {
                ResultStatus = ResultStatus.Error,
                Page = pages,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

 
        public async Task<IDataResult<PageUpdateDto>> GetUpdateDto(int pageId)
        {
            var page = await _unitOfWork.Page.GetAsync(x => x.Id == pageId);
            if (page != null)
            {
                var pageUpdateDto = _mapper.Map<PageUpdateDto>(page);
                pageUpdateDto.Url = SeoHelper.ToSeoUrl(pageUpdateDto.PageTitle);
                return new DataResult<PageUpdateDto>(ResultStatus.Success, pageUpdateDto);
            }
            return new DataResult<PageUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int pageId)
        {
            var page = await _unitOfWork.Page.GetAsync(x => x.Id == pageId);
            if (page != null)
            {
                await _unitOfWork.Page.DeleteAsync(page);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{page.PageTitle} isimli sayfa başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<PageDto>> Update(PageUpdateDto pageUpdateDto, string modifiedByName)
        {
            var page = _mapper.Map<Page>(pageUpdateDto);
            page.Url = SeoHelper.ToSeoUrl(pageUpdateDto.PageTitle);
            var updatedPage = await _unitOfWork.Page.UpdateAsync(page);
            await _unitOfWork.SaveAsync();
            return new DataResult<PageDto>(ResultStatus.Success, $"{updatedPage.PageTitle} isimli sayfa başarılı bir şekilde güncellenmiştir.", new PageDto
            {
                ResultStatus = ResultStatus.Success,
                Page = updatedPage,
                Message = $"{updatedPage.PageTitle} isimli sayfa başarılı bir şekilde güncellenmiştir."
            });
        }
 
    }
}

