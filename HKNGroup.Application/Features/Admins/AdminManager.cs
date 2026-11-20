using AutoMapper;

namespace HKNGroup.Application.Features.Admins
{
    public class AdminManager : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public AdminManager(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<IDataResult<AdminDto>> Authenticate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return DataResult<AdminDto>.Invalid("Kullanıcı adı ve parola zorunludur.");
            }

            var admin = await _unitOfWork.Admin.GetAsync(x => x.UserName == userName && x.IsActive && !x.IsDeleted);
            if (admin == null)
            {
                return DataResult<AdminDto>.NotFound("Geçersiz kullanıcı adı veya şifre.");
            }

            bool isValid = _passwordHasher.VerifyPassword(admin.Password, password);
            if (!isValid)
            {
                return DataResult<AdminDto>.Unauthorized("Geçersiz kullanıcı adı veya şifre.");
            }

            // Düz metin parolanın ilk girişte otomatik olarak güvenli hash'e dönüştürülmesi
            if (admin.Password == password)
            {
                admin.Password = _passwordHasher.HashPassword(password);
                admin.Touch();
                await _unitOfWork.Admin.UpdateAsync(admin);
                await _unitOfWork.SaveAsync();
            }

            return DataResult<AdminDto>.Success(new AdminDto
            {
                Admin = admin,
                ResultStatus = ResultStatus.Success,
                Message = "Giriş başarılı."
            });
        }

        public async Task<IDataResult<AdminDto>> Get(int Id = 1)
        {
            var admin = await _unitOfWork.Admin.GetAsync(x => x.Id == Id && !x.IsDeleted);
            if (admin == null)
            {
                return DataResult<AdminDto>.NotFound("Yönetici bulunamadı.");
            }

            return DataResult<AdminDto>.Success(new AdminDto
            {
                Admin = admin,
                ResultStatus = ResultStatus.Success,
                Message = "İşlem başarılı."
            });
        }

        public async Task<IDataResult<AdminDto>> GetByUserName(string userName)
        {
            var admin = await _unitOfWork.Admin.GetAsync(x => x.UserName == userName && !x.IsDeleted);
            if (admin == null)
            {
                return DataResult<AdminDto>.NotFound("Yönetici bulunamadı.");
            }

            return DataResult<AdminDto>.Success(new AdminDto
            {
                Admin = admin,
                ResultStatus = ResultStatus.Success,
                Message = "İşlem başarılı."
            });
        }

        public async Task<IDataResult<AdminUpdateDto>> GetUpdateDto(int adminId)
        {
            var admin = await _unitOfWork.Admin.GetAsync(x => x.Id == adminId && !x.IsDeleted);
            if (admin != null)
            {
                var adminUpdateDto = _mapper.Map<AdminUpdateDto>(admin);
                return DataResult<AdminUpdateDto>.Success(adminUpdateDto);
            }
            return DataResult<AdminUpdateDto>.NotFound("Hata, kayıt bulunamadı!");
        }

        public async Task<IDataResult<AdminDto>> Update(AdminUpdateDto adminUpdateDto, string modifiedByName)
        {
            var currentAdmin = await _unitOfWork.Admin.GetAsync(x => x.Id == adminUpdateDto.Id && !x.IsDeleted);
            if (currentAdmin == null)
            {
                return DataResult<AdminDto>.NotFound("Yönetici bulunamadı.");
            }

            _mapper.Map(adminUpdateDto, currentAdmin);

            // Eğer parola güncellenmiş ve hash formatında değilse hashle
            if (!string.IsNullOrWhiteSpace(adminUpdateDto.Password) && adminUpdateDto.Password != currentAdmin.Password)
            {
                currentAdmin.Password = _passwordHasher.HashPassword(adminUpdateDto.Password);
            }

            currentAdmin.Touch();
            await _unitOfWork.Admin.UpdateAsync(currentAdmin);
            await _unitOfWork.SaveAsync();

            return DataResult<AdminDto>.Success(new AdminDto
            {
                Admin = currentAdmin,
                ResultStatus = ResultStatus.Success,
                Message = "İşlem başarılı."
            });
        }

        public async Task<IResult> Delete(int adminId, string modifiedByName)
        {
            var admin = await _unitOfWork.Admin.GetAsync(x => x.Id == adminId);
            if (admin == null)
            {
                return Result.NotFound("Yönetici bulunamadı.");
            }

            admin.MarkAsDeleted();
            await _unitOfWork.Admin.UpdateAsync(admin);
            await _unitOfWork.SaveAsync();
            return Result.Success("Yönetici silindi.");
        }

        public async Task<IResult> HardDelete(int adminId)
        {
            var admin = await _unitOfWork.Admin.GetAsync(x => x.Id == adminId);
            if (admin == null)
            {
                return Result.NotFound("Yönetici bulunamadı.");
            }

            await _unitOfWork.Admin.DeleteAsync(admin);
            await _unitOfWork.SaveAsync();
            return Result.Success("Yönetici kalıcı olarak silindi.");
        }
    }
}

