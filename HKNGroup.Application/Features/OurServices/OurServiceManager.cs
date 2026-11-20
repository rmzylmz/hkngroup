using AutoMapper;
namespace HKNGroup.Application.Features.OurServices
{
    public class OurServiceManager : IOurServicesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OurServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<OurServicesDto>> Add(OurServicesAddDto ourServicesAddDto)
        {
            var ourServices = _mapper.Map<OurService>(ourServicesAddDto);
            ourServices.ModifiedTime = DateTime.Now;
            ourServices.Url = SeoHelper.ToSeoUrl(ourServicesAddDto.Title);
            var addedServices = await _unitOfWork.OurServices.AddAsync(ourServices);
            await _unitOfWork.SaveAsync();
            return new DataResult<OurServicesDto>(ResultStatus.Success, $"{addedServices.Title} isimli hizmet başarılı bir şekilde kayıt edilmiştir.", new OurServicesDto
            {
                OurServices = addedServices,
                Message = $"{addedServices.Title} isimli hizmet başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int ourServicesId)
        {
            var ourServices = await _unitOfWork.OurServices.GetAsync(x => x.Id == ourServicesId);
            if (ourServices != null)
            {
                ourServices.IsDeleted = true;
                ourServices.ModifiedTime = DateTime.Now;
                await _unitOfWork.OurServices.UpdateAsync(ourServices);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{ourServices.Title} isimli hizmet başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<OurServicesDto>> Get(int ourServicesId)
        {
            var ourServices = await _unitOfWork.OurServices.GetAsync(x => x.Id == ourServicesId);
            if (ourServices != null)
            {
                return new DataResult<OurServicesDto>(ResultStatus.Success, new OurServicesDto
                {
                    OurServices = ourServices,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<OurServicesDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new OurServicesDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                OurServices = null
            });
        }

        public async Task<IDataResult<OurServicesListDto>> GetAll()
        {
            var ourServicess = await _unitOfWork.OurServices.GetAllAsync();
            if (ourServicess != null)
            {
                return new DataResult<OurServicesListDto>(ResultStatus.Success, new OurServicesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    OurServices = ourServicess
                });
            }
            return new DataResult<OurServicesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new OurServicesListDto
            {
                ResultStatus = ResultStatus.Error,
                OurServices = ourServicess,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<OurServicesListDto>> GetAllByNonDelete()
        {
            var ourServicess = await _unitOfWork.OurServices.GetAllAsync(x => x.IsDeleted == false);
            if (ourServicess != null)
            {
                return new DataResult<OurServicesListDto>(ResultStatus.Success, new OurServicesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    OurServices = ourServicess
                });
            }
            return new DataResult<OurServicesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new OurServicesListDto
            {
                ResultStatus = ResultStatus.Error,
                OurServices = ourServicess,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<OurServicesListDto>> GetAllByNonDeleteAndActive()
        {
            var ourServicess = await _unitOfWork.OurServices.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (ourServicess != null)
            {
                return new DataResult<OurServicesListDto>(ResultStatus.Success, new OurServicesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    OurServices = ourServicess
                });
            }
            return new DataResult<OurServicesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new OurServicesListDto
            {
                ResultStatus = ResultStatus.Error,
                OurServices = ourServicess,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }


        public async Task<IDataResult<OurServicesDto>> GetString(string name)
        {
            var ourServices = await _unitOfWork.OurServices.GetAsync(x => x.Url == name);

            if (ourServices != null)
            {
                return new DataResult<OurServicesDto>(ResultStatus.Success, new OurServicesDto
                {
                    OurServices = ourServices,
                    ResultStatus = ResultStatus.Success,
                });
            }
            return new DataResult<OurServicesDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new OurServicesDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                OurServices = null
            });
        }

        public async Task<IDataResult<OurServicesUpdateDto>> GetUpdateDto(int ourServicesId)
        {
            var ourServices = await _unitOfWork.OurServices.GetAsync(x => x.Id == ourServicesId);

            if (ourServices != null)
            {
                var ourServicesUpdateDto = _mapper.Map<OurServicesUpdateDto>(ourServices);
                return new DataResult<OurServicesUpdateDto>(ResultStatus.Success, ourServicesUpdateDto);
            }
            return new DataResult<OurServicesUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int ourServicesId)
        {
            var ourServices = await _unitOfWork.OurServices.GetAsync(x => x.Id == ourServicesId);
            if (ourServices != null)
            {
                await _unitOfWork.OurServices.DeleteAsync(ourServices);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{ourServices.Title} isimli hizmet başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<OurServicesDto>> Update(OurServicesUpdateDto ourServicesUpdateDto)
        {
            var ourServices = _mapper.Map<OurService>(ourServicesUpdateDto);
            ourServices.Url = SeoHelper.ToSeoUrl(ourServicesUpdateDto.Title);
            var updatedServices = await _unitOfWork.OurServices.UpdateAsync(ourServices);

            await _unitOfWork.SaveAsync();
            return new DataResult<OurServicesDto>(ResultStatus.Success, $"{updatedServices.Title} isimli hizmet başarılı bir şekilde güncellenmiştir.", new OurServicesDto
            {
                ResultStatus = ResultStatus.Success,
                OurServices = updatedServices,
                Message = $"{updatedServices.Title} isimli hizmet başarılı bir şekilde güncellenmiştir."
            });
        }
    }
}

