namespace HKNGroup.Application.Common.Results
{
    public interface IDataResult<out T> : IResult
    {
        public T? Data { get; }
    }
}

