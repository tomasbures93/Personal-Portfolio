using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common.Results;

namespace Portfolio.API.Extensions;

public static class ControllerResultExtension
{
    public static ActionResult ReturnActionResult(this ControllerBase controller, Result result)
    {
        return result.Status switch
        {
            ResultStatus.Success => controller.Ok(result),
            ResultStatus.ValidationError => controller.BadRequest(result),
            ResultStatus.NotFound => controller.NotFound(result),
            ResultStatus.Conflict => controller.Conflict(result),
            ResultStatus.Unauthorized => controller.Unauthorized(result),
            ResultStatus.Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, result),
            ResultStatus.Error => controller.StatusCode(StatusCodes.Status500InternalServerError, result),
            _ => controller.BadRequest(result)
        };
    }

    public static ActionResult ReturnActionResult<T>(this ControllerBase controller, Result<T> result)
    {
        return result.Status switch
        {
            ResultStatus.Success => controller.Ok(result.Value),
            ResultStatus.ValidationError => controller.BadRequest(result),
            ResultStatus.NotFound => controller.NotFound(result),
            ResultStatus.Conflict => controller.Conflict(result),
            ResultStatus.Unauthorized => controller.Unauthorized(result),
            ResultStatus.Forbidden => controller.StatusCode(StatusCodes.Status403Forbidden, result),
            ResultStatus.Error => controller.StatusCode(StatusCodes.Status500InternalServerError, result),
            _ => controller.BadRequest(result)
        };
    }
}
