using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HKNGroup.Application.Features.Sliders
{
    public class SliderManager : ISliderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SliderManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<SliderDto>> Add(SliderAddDto homePageSliderAddDto, string createdByName)
        {
            var slider = _mapper.Map<Slider>(homePageSliderAddDto);
            slider.ModifiedTime = DateTime.Now;
            var addedSlider = await _unitOfWork.Slider.AddAsync(slider);
            await _unitOfWork.SaveAsync();
            return new DataResult<SliderDto>(ResultStatus.Success, $"{addedSlider.Title} başlıklı slider başarılı bir şekilde kayıt edilmiştir.", new SliderDto
            {
                Slider = addedSlider,
                Message = $"{addedSlider.Title} başlıklı slider başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int homePageSliderId, string modifiedByName)
        {
            var slider = await _unitOfWork.Slider.GetAsync(x => x.Id == homePageSliderId);
            if (slider != null)
            {
                slider.IsDeleted = true;
                slider.ModifiedTime = DateTime.Now;
                await _unitOfWork.Slider.UpdateAsync(slider);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{slider.Title} başlıklı slider başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<SliderDto>> Get(int homePageSliderId)
        {
            var slider = await _unitOfWork.Slider.GetAsync(x => x.Id == homePageSliderId);
            if (slider != null)
            {
                return new DataResult<SliderDto>(ResultStatus.Success, new SliderDto
                {
                    ResultStatus = ResultStatus.Success,
                    Slider = slider,

                });
            }
            return new DataResult<SliderDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new SliderDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                Slider = null
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAll()
        {
            var sliders = await _unitOfWork.Slider.GetAllAsync();
            if (sliders != null)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Sliders = sliders,
                });
            }
            return new DataResult<SliderListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new SliderListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Sliders = null
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAllByNonDelete()
        {
            var sliders = await _unitOfWork.Slider.GetAllAsync(x => x.IsDeleted == false);
            if (sliders != null)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Sliders = sliders,
                });
            }
            return new DataResult<SliderListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new SliderListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Sliders = null
            });
        }

        public async Task<IDataResult<SliderListDto>> GetAllByNonDeleteAndActive()
        {
            var sliders = await _unitOfWork.Slider.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (sliders != null)
            {
                return new DataResult<SliderListDto>(ResultStatus.Success, new SliderListDto
                {
                    ResultStatus = ResultStatus.Success,
                    Sliders = sliders,
                });
            }
            return new DataResult<SliderListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new SliderListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                Sliders = null
            });
        }

        public async Task<IDataResult<SliderUpdateDto>> GetUpdateDto(int homePageSliderId)
        {
            var slider = await _unitOfWork.Slider.GetAsync(x => x.Id == homePageSliderId);
            if (slider != null)
            {
                var sliderUpdateDto = _mapper.Map<SliderUpdateDto>(slider);
                return new DataResult<SliderUpdateDto>(ResultStatus.Success,sliderUpdateDto);
            }
            return new DataResult<SliderUpdateDto>(ResultStatus.Error,"Hata, kayıt bulunamadı!",null);
        }

        public async Task<IResult> HardDelete(int homePageSliderId)
        {
            var slider = await _unitOfWork.Slider.GetAsync(x => x.Id == homePageSliderId);
            if (slider != null)
            {
                await _unitOfWork.Slider.DeleteAsync(slider);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{slider.Title} başlıklı slider başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<SliderDto>> Update(SliderUpdateDto homePageSliderUpdateDto, string modifiedByName)
        {
            var slider = _mapper.Map<Slider>(homePageSliderUpdateDto);
            var updatedSlider = await _unitOfWork.Slider.UpdateAsync(slider);
            await _unitOfWork.SaveAsync();
            return new DataResult<SliderDto>(ResultStatus.Success, $"{updatedSlider.Title} başlıklı slider başarılı bir şekilde güncellenmiştir.", new SliderDto
            {
                Slider = slider,
                ResultStatus = ResultStatus.Success,
                Message = $"{updatedSlider.Title} başlıklı slider başarılı bir şekilde güncellenmiştir."
            });
        }
    }
}

