using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text;

namespace SmartSchool.API.Componentes
{
	public static class ControllerBaseExtension
    {
        public static IActionResult ProcessResult(this ControllerBase controller, IResult result)
        {
            return result.Status switch
            {
                ResultStatus.Ok => result.GetValue() == null ? controller.Ok() : controller.Ok(result.GetValue()),
                ResultStatus.Created => controller.Created("", result.GetValue()),
                ResultStatus.NotFound => controller.NotFound(),
                ResultStatus.Unauthorized => controller.Unauthorized(),
                ResultStatus.Forbidden => controller.Forbid(),
                ResultStatus.BadRequest => ProcessBadRequest(controller, result),
                ResultStatus.UnprocessableEntity => ProcessUnprocessableEntity(controller, result),
                ResultStatus.InternalServerError => controller.StatusCode((int)System.Net.HttpStatusCode.InternalServerError, result.Errors),
                _ => throw new NotSupportedException($"Result {result.Status} conversion is not supported."),
            };
        }

        private static IActionResult ProcessBadRequest(ControllerBase controller, IResult result)
        {
            foreach (ValidationError validationError in result.ValidationErrors)
            {
                controller.ModelState.AddModelError(validationError.Identifier, validationError.ErrorMessage);
            }

            var problemDetails = new ValidationProblemDetails(controller.ModelState)
            {
                Instance = controller.HttpContext.Request.Path,
                Status = StatusCodes.Status400BadRequest,
                Detail = "Please refer to the errors property for additional details"
            };

            return controller.BadRequest(problemDetails);
        }

        private static IActionResult ProcessUnprocessableEntity(ControllerBase controller, IResult result)
        {
            StringBuilder stringBuilder = new StringBuilder("Next error(s) occured:");
            foreach (string error in result.Errors)
            {
                stringBuilder.Append("* ").Append(error).AppendLine();
            }

            return controller.UnprocessableEntity(new ProblemDetails
            {
                Title = "Something went wrong.",
                Detail = stringBuilder.ToString()
            });
        }
    }
}
