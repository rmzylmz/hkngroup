using System;
using System.Collections.Generic;

namespace HKNGroup.Application.Common.Results
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception? Exception { get; }
        public bool IsSuccess => ResultStatus == ResultStatus.Success;
        public IReadOnlyList<string> Errors { get; }
    }
}

