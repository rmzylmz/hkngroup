
namespace HKNGroup.Application.Common.Dtos
{
    public abstract class DtoGetBase
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; } = "";
    }
}

