using AutoMapper;


namespace HKNGroup.Application.Features.ArticleComments
{
    public class ArticleCommentManager : IArticleCommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArticleCommentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<ArticleCommentDto>> Add(ArticleCommentAddDto commentAddDto, string createdByName)
        {
            var comment = _mapper.Map<ArticleComment>(commentAddDto);
            comment.ModifiedTime = DateTime.Now;
            var addedComment = await _unitOfWork.ArticleComment.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleCommentDto>(ResultStatus.Success, "İşlem başarılı..", new ArticleCommentDto
            {
                Comment = addedComment,
                Message = "İşlem başarılı..",
                ResultStatus = ResultStatus.Success
            });
        }

        public async Task<IResult> Delete(int commentId, string modifiedByName)
        {
            var comment = await _unitOfWork.ArticleComment.GetAsync(x => x.Id == commentId);
            if (comment != null)
            {
                comment.IsDeleted = true;
                comment.ModifiedTime = DateTime.Now;
                await _unitOfWork.ArticleComment.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "İşlem başarılı..");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IResult> DoActive(int commentId, string modifiedByName)
        {
            var comment = await _unitOfWork.ArticleComment.GetAsync(x => x.Id == commentId);
            if (comment != null)
            {
                comment.IsActive = true;
                await _unitOfWork.ArticleComment.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{comment.FirstName} isimli kişinin yorumu başarılı bir şekilde onaylanmıştır.");
            }
            return new Result(ResultStatus.Error, "Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<ArticleCommentDto>> Get(int commentId)
        {
            var comment = await _unitOfWork.ArticleComment.GetAsync(x => x.Id == commentId, x => x.Article);
            if (comment != null)
            {
                return new DataResult<ArticleCommentDto>(ResultStatus.Success, new ArticleCommentDto
                {
                    Comment = comment,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCommentDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", new ArticleCommentDto
            {
                Comment = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hata, kayıt bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCommentListDto>> GetAll()
        {
            var comments = await _unitOfWork.ArticleComment.GetAllAsync(null, x => x.Article);
            if (comments != null)
            {
                return new DataResult<ArticleCommentListDto>(ResultStatus.Success, new ArticleCommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCommentListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCommentListDto
            {
                ResultStatus = ResultStatus.Error,
                Comments = null,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCommentListDto>> GetAllByNonDelete()
        {
            var comments = await _unitOfWork.ArticleComment.GetAllAsync(x => x.IsDeleted == false, x => x.Article);
            if (comments != null)
            {
                return new DataResult<ArticleCommentListDto>(ResultStatus.Success, new ArticleCommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCommentListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCommentListDto
            {
                ResultStatus = ResultStatus.Error,
                Comments = null,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCommentListDto>> GetAllByNonDeleteAndActive()
        {
            var comments = await _unitOfWork.ArticleComment.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true, x => x.Article);
            if (comments != null)
            {
                return new DataResult<ArticleCommentListDto>(ResultStatus.Success, new ArticleCommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCommentListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCommentListDto
            {
                ResultStatus = ResultStatus.Error,
                Comments = null,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCommentListDto>> GetAllByNonDeleteAndActiveSite()
        {
            var comments = await _unitOfWork.ArticleComment.GetAllAsync(x => x.IsDeleted == false && x.IsActive == true , x => x.Article);
            if (comments != null)
            {
                return new DataResult<ArticleCommentListDto>(ResultStatus.Success, new ArticleCommentListDto
                {
                    Comments = comments,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleCommentListDto>(ResultStatus.Error, "Hata, kayıtlar bulunamadı!", new ArticleCommentListDto
            {
                ResultStatus = ResultStatus.Error,
                Comments = null,
                Message = "Hata, kayıtlar bulunamadı!"
            });
        }

        public async Task<IDataResult<ArticleCommentUpdateDto>> GetUpdateDto(int commentId)
        {
            var comment = await _unitOfWork.ArticleComment.GetAsync(x => x.Id == commentId, x => x.Article);
            if (comment != null)
            {
                var commentUpdateDto = _mapper.Map<ArticleCommentUpdateDto>(comment);
                return new DataResult<ArticleCommentUpdateDto>(ResultStatus.Success, commentUpdateDto);
            }
            return new DataResult<ArticleCommentUpdateDto>(ResultStatus.Error, "Hata, kayıt bulunamadı!", null);
        }

        public async Task<IResult> HardDelete(int commentId)
        {
            var comment = await _unitOfWork.ArticleComment.GetAsync(x => x.Id == commentId);
            if (comment != null)
            {
                await _unitOfWork.ArticleComment.DeleteAsync(comment);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "İşlem başarılı..");
            }
            return new Result(ResultStatus.Error, "Hata kayıt bulunamadı!");
        }

        public async Task<IDataResult<ArticleCommentDto>> Update(ArticleCommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            var comment = _mapper.Map<ArticleComment>(commentUpdateDto);
            var updatedComment = await _unitOfWork.ArticleComment.UpdateAsync(comment);
            await _unitOfWork.SaveAsync();
            return new DataResult<ArticleCommentDto>(ResultStatus.Success, new ArticleCommentDto
            {
                Comment = updatedComment,
                ResultStatus = ResultStatus.Success,

            });
        }
    }
}

