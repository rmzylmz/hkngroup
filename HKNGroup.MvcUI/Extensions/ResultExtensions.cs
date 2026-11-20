using Microsoft.AspNetCore.Mvc;
using System;
using IAppResult = HKNGroup.Application.Common.Results.IResult;

namespace HKNGroup.WebUI.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Controller controller, IDataResult<T> result, Func<T, IActionResult>? onSuccess = null)
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => onSuccess != null ? onSuccess(result.Data) : controller.View(result.Data),
                ResultStatus.NotFound => controller.NotFound(),
                ResultStatus.Unauthorized => controller.Unauthorized(),
                ResultStatus.Invalid => controller.BadRequest(result.Message),
                _ => controller.NotFound()
            };
        }

        public static IActionResult ToActionResult(this Controller controller, IAppResult result, IActionResult onSuccess)
        {
            return result.ResultStatus switch
            {
                ResultStatus.Success => onSuccess,
                ResultStatus.NotFound => controller.NotFound(),
                ResultStatus.Unauthorized => controller.Unauthorized(),
                ResultStatus.Invalid => controller.BadRequest(result.Message),
                _ => controller.NotFound()
            };
        }
    }
}
