using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HKNGroup.Application.Features.References
{
    public class ReferanceManager : IReferanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReferanceManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<ReferanceDto>> Add(ReferanceAddDto referanceAddDto, string createdByName)
        {
            var referance = _mapper.Map<Referance>(referanceAddDto);
            referance.ModifiedTime = DateTime.Now;
            var addedSlider = await _unitOfWork.Referances.AddAsync(referance);
            await _unitOfWork.SaveAsync();
            return new DataResult<ReferanceDto>(ResultStatus.Success, $"{addedSlider.Title} başlıklı referance başarılı bir şekilde kayıt edilmiştir.", new ReferanceDto
            {
                Referances = addedSlider,
                Message = $"{addedSlider.Title} başlıklı referance başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int referanceId, string modifiedByName)
        {
            var referance = await _unitOfWork.Referances.GetAsync(x => x.Id == referanceId);
            if (referance != null)
            {
                referance.IsDeleted = true;
                referance.ModifiedTime = DateTime.Now;
                await _unitOfWork.Referances.UpdateAsync(referance);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{referance.Title} başlıklı referance başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ReferanceDto>> Get(int referanceId)
        {
            var referance = await _unitOfWork.Referances.GetAsync(x => x.Id == referanceId);
            if (referance != null)
            {
                return new DataResult<ReferanceDto>(ResultStatus.Success, new ReferanceDto
                {
                    ResultStatus = ResultStatus.Success,
                    Referances = referance,

                });
            }
            return new DataResult<ReferanceDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ReferanceDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                Referances = null
            });
        }

        public async Task<IDataResult<ReferanceListDto>> GetAll()
        {
            var referances = await _unitOfWork.Referances.GetAllAsync();
            if (referances != null)
            {
                return new DataResult<ReferanceListDto>(ResultStatus.Success, new ReferanceListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Referances = referances,
                });
            }
            return new DataResult<ReferanceListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ReferanceListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Referances = null
            });
        }

        public async Task<IDataResult<ReferanceListDto>> GetAllByNonDelete()
        {
            var referances = await _unitOfWork.Referances.GetAllAsync(x => x.IsDeleted == false);
            if (referances != null)
            {
                return new DataResult<ReferanceListDto>(ResultStatus.Success, new ReferanceListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Referances = referances,
                });
            }
            return new DataResult<ReferanceListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ReferanceListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Referances = null
            });
        }

        public async Task<IDataResult<ReferanceListDto>> GetAllByNonDeleteAndActive()
        {
            var referances = await _unitOfWork.Referances.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (referances != null)
            {
                return new DataResult<ReferanceListDto>(ResultStatus.Success, new ReferanceListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Referances = referances,
                });
            }
            return new DataResult<ReferanceListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ReferanceListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Referances = null
            });
        }

        public async Task<IResult> HardDelete(int referanceId)
        {
            var referance = await _unitOfWork.Referances.GetAsync(x => x.Id == referanceId);
            if (referance != null)
            {
                await _unitOfWork.Referances.DeleteAsync(referance);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{referance.Title} başlıklı referance başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }


        public async Task<IDataResult<ReferanceUpdateDto>> GetUpdateDto(int referanceId)
        {
            var referance = await _unitOfWork.Referances.GetAsync(x => x.Id == referanceId);
            if (referance != null)
            {
                var referanceUpdateDto = _mapper.Map<ReferanceUpdateDto>(referance);
                return new DataResult<ReferanceUpdateDto>(ResultStatus.Success,referanceUpdateDto);
            }
            return new DataResult<ReferanceUpdateDto>(ResultStatus.Error,"Hata, kayıt bulunamadı!",null);
        }


        public async Task<IDataResult<ReferanceDto>> Update(ReferanceUpdateDto referanceUpdateDto, string modifiedByName)
        {
            var referance = _mapper.Map<Referance>(referanceUpdateDto);
            var updatedSlider = await _unitOfWork.Referances.UpdateAsync(referance);
            await _unitOfWork.SaveAsync();
            return new DataResult<ReferanceDto>(ResultStatus.Success, $"{updatedSlider.Title} başlıklı referance başarılı bir şekilde güncellenmiştir.", new ReferanceDto
            {
                Referances = referance,
                ResultStatus = ResultStatus.Success,
                Message = $"{updatedSlider.Title} başlıklı referance başarılı bir şekilde güncellenmiştir."
            });
        }
    }
}

