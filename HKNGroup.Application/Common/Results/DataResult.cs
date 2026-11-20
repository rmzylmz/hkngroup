using System;
using System.Collections.Generic;

namespace HKNGroup.Application.Common.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult(ResultStatus resultStatus, T? data)
        {
            ResultStatus = resultStatus;
            Data = data;
            Errors = Array.Empty<string>();
        }

        public DataResult(ResultStatus resultStatus, string message, T? data) : this(resultStatus, data)
        {
            Message = message;
        }

        public DataResult(ResultStatus resultStatus, string message, T? data, Exception? exception) : this(resultStatus, message, data)
        {
            Exception = exception;
        }

        public DataResult(ResultStatus resultStatus, string message, T? data, IEnumerable<string>? errors) : this(resultStatus, message, data)
        {
            Errors = errors != null ? new List<string>(errors) : Array.Empty<string>();
        }

        public T? Data { get; }
        public ResultStatus ResultStatus { get; }
        public string Message { get; } = string.Empty;
        public Exception? Exception { get; }
        public IReadOnlyList<string> Errors { get; } = Array.Empty<string>();

        public static DataResult<T> Success(T data, string message = "İşlem başarılı.") => new(ResultStatus.Success, message, data);
        public static DataResult<T> Error(string message, T? data = default, Exception? ex = null) => new(ResultStatus.Error, message, data, ex);
        public static DataResult<T> NotFound(string message = "Kayıt bulunamadı.", T? data = default) => new(ResultStatus.NotFound, message, data);
        public static DataResult<T> Invalid(string message, IEnumerable<string>? errors = null, T? data = default) => new(ResultStatus.Invalid, message, data, errors);
        public static DataResult<T> Unauthorized(string message = "Yetkisiz erişim.", T? data = default) => new(ResultStatus.Unauthorized, message, data);
    }
}

