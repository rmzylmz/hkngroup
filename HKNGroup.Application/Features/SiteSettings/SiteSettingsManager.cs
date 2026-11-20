using AutoMapper;

namespace HKNGroup.Application.Features.SiteSettings
{
    public class SiteSettingsManager : ISiteSettingsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SiteSettingsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<SiteSettingsUpdateDto>> GetUpdateDto(int siteSettingId)
        {
            var siteSetting = await _unitOfWork.SiteSettings.GetAsync(x => x.Id == siteSettingId);
            if (siteSetting != null)
            {               
                var siteSettingUpdateDto = _mapper.Map<SiteSettingsUpdateDto>(siteSetting);
                return new DataResult<SiteSettingsUpdateDto>(ResultStatus.Success, siteSettingUpdateDto);
            }
            return new DataResult<SiteSettingsUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IDataResult<SiteSettingsDto>> Update(SiteSettingsUpdateDto siteSettingUpdateDto)
        {
            var siteSetting = _mapper.Map<SiteSetting>(siteSettingUpdateDto);
            var updatedSiteSetting = await _unitOfWork.SiteSettings.UpdateAsync(siteSetting);
            await _unitOfWork.SaveAsync();

            var dto = _mapper.Map<SiteSettingsDto>(updatedSiteSetting);
            dto.ResultStatus = ResultStatus.Success;
            dto.Message = $"{updatedSiteSetting.SiteDefaultTitle} başlıklı site bilgisi başarılı bir şekilde güncellenmiştir.";

            return new DataResult<SiteSettingsDto>(ResultStatus.Success, dto.Message, dto);
        }
    }
}

