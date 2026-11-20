using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HKNGroup.Application.Features.PopupMessages
{
    public class PopupMessagesManager : IPopupMessagesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PopupMessagesManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<PopupMessagesDto>> Add(PopupMessagesAddDto PopupMessagesAddDto, string createdByName)
        {
            var popupMessage = _mapper.Map<HKNGroup.Domain.Entities.PopupMessages>(PopupMessagesAddDto);
            popupMessage.ModifiedTime = DateTime.Now;
            var addedPopupMessage = await _unitOfWork.PopupMessages.AddAsync(popupMessage);
            await _unitOfWork.SaveAsync();
            return new DataResult<PopupMessagesDto>(ResultStatus.Success, $"{addedPopupMessage.Title} başlıklı açılır mesaj başarılı bir şekilde kayıt edilmiştir.", new PopupMessagesDto
            {
                PopupMessages = addedPopupMessage,
                Message = $"{addedPopupMessage.Title} başlıklı popupMessage başarılı bir şekilde kayıt edilmiştir.",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int PopupMessagesId, string modifiedByName)
        {
            var popupMessage = await _unitOfWork.PopupMessages.GetAsync(x => x.Id == PopupMessagesId);
            if (popupMessage != null)
            {
                popupMessage.IsDeleted = true;
                popupMessage.ModifiedTime = DateTime.Now;
                await _unitOfWork.PopupMessages.UpdateAsync(popupMessage);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{popupMessage.Title} başlıklı açılır mesaj başarılı bir şekilde silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<PopupMessagesDto>> Get(int PopupMessagesId)
        {
            var popupMessage = await _unitOfWork.PopupMessages.GetAsync(x => x.Id == PopupMessagesId);
            if (popupMessage != null)
            {
                return new DataResult<PopupMessagesDto>(ResultStatus.Success, new PopupMessagesDto
                {
                    ResultStatus = ResultStatus.Success,
                    PopupMessages = popupMessage,

                });
            }
            return new DataResult<PopupMessagesDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new PopupMessagesDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!",
                PopupMessages = null
            });
        }

        public async Task<IDataResult<PopupMessagesListDto>> GetAll()
        {
            var popupMessages = await _unitOfWork.PopupMessages.GetAllAsync();
            if (popupMessages != null)
            {
                return new DataResult<PopupMessagesListDto>(ResultStatus.Success, new PopupMessagesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    PopupMessages = popupMessages,
                });
            }
            return new DataResult<PopupMessagesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PopupMessagesListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                PopupMessages = null
            });
        }

        public async Task<IDataResult<PopupMessagesListDto>> GetAllByNonDelete()
        {
            var popupMessages = await _unitOfWork.PopupMessages.GetAllAsync(x => x.IsDeleted == false);
            if (popupMessages != null)
            {
                return new DataResult<PopupMessagesListDto>(ResultStatus.Success, new PopupMessagesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    PopupMessages = popupMessages,
                });
            }
            return new DataResult<PopupMessagesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PopupMessagesListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                PopupMessages = null
            });
        }

        public async Task<IDataResult<PopupMessagesListDto>> GetAllByNonDeleteAndActive()
        {
            var popupMessages = await _unitOfWork.PopupMessages.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true);
            if (popupMessages != null)
            {
                return new DataResult<PopupMessagesListDto>(ResultStatus.Success, new PopupMessagesListDto
                {
                    ResultStatus = ResultStatus.Success,
                    PopupMessages = popupMessages,
                });
            }
            return new DataResult<PopupMessagesListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new PopupMessagesListDto
            {
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıtlar bulunamadı!",
                PopupMessages = null
            });
        }

        public async Task<IDataResult<PopupMessagesUpdateDto>> GetUpdateDto(int PopupMessagesId)
        {
            var popupMessage = await _unitOfWork.PopupMessages.GetAsync(x => x.Id == PopupMessagesId);
            if (popupMessage != null)
            {
                var popupMessageUpdateDto = _mapper.Map<PopupMessagesUpdateDto>(popupMessage);
                return new DataResult<PopupMessagesUpdateDto>(ResultStatus.Success,popupMessageUpdateDto);
            }
            return new DataResult<PopupMessagesUpdateDto>(ResultStatus.Error,"Hata, kayıt bulunamadı!",null);
        }

        public async Task<IResult> HardDelete(int PopupMessagesId)
        {
            var popupMessage = await _unitOfWork.PopupMessages.GetAsync(x => x.Id == PopupMessagesId);
            if (popupMessage != null)
            {
                await _unitOfWork.PopupMessages.DeleteAsync(popupMessage);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{popupMessage.Title} başlıklı açılır mesaj  başarılı bir şekilde veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<PopupMessagesDto>> Update(PopupMessagesUpdateDto PopupMessagesUpdateDto, string modifiedByName)
        {
            var popupMessage = _mapper.Map<HKNGroup.Domain.Entities.PopupMessages>(PopupMessagesUpdateDto);
            var updatedSlider = await _unitOfWork.PopupMessages.UpdateAsync(popupMessage);
            await _unitOfWork.SaveAsync();
            return new DataResult<PopupMessagesDto>(ResultStatus.Success, $"{updatedSlider.Title} başlıklı açılır mesaj başarılı bir şekilde güncellenmiştir.", new PopupMessagesDto
            {
                PopupMessages = popupMessage,
                ResultStatus = ResultStatus.Success,
                Message = $"{updatedSlider.Title} başlıklı açılır mesaj başarılı bir şekilde güncellenmiştir."
            });
        }
    }
}


