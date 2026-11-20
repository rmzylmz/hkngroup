using System;
using System.Collections.Generic;

namespace HKNGroup.Application.Common.Results
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
            Errors = Array.Empty<string>();
        }

        public Result(ResultStatus resultStatus, string message) : this(resultStatus)
        {
            Message = message;
        }

        public Result(ResultStatus resultStatus, string message, Exception? exception) : this(resultStatus, message)
        {
            Exception = exception;
        }

        public Result(ResultStatus resultStatus, string message, IEnumerable<string>? errors) : this(resultStatus, message)
        {
            Errors = errors != null ? new List<string>(errors) : Array.Empty<string>();
        }

        public ResultStatus ResultStatus { get; }
        public string Message { get; } = string.Empty;
        public Exception? Exception { get; }
        public IReadOnlyList<string> Errors { get; } = Array.Empty<string>();

        public static Result Success(string message = "İşlem başarılı.") => new(ResultStatus.Success, message);
        public static Result Error(string message, Exception? ex = null) => new(ResultStatus.Error, message, ex);
        public static Result NotFound(string message = "Kayıt bulunamadı.") => new(ResultStatus.NotFound, message);
        public static Result Invalid(string message, IEnumerable<string>? errors = null) => new(ResultStatus.Invalid, message, errors ?? Array.Empty<string>());
        public static Result Unauthorized(string message = "Yetkisiz erişim.") => new(ResultStatus.Unauthorized, message);
    }
}

