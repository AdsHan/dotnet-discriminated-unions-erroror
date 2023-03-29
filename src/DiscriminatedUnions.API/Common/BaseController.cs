using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace DiscriminatedUnions.API.Common
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        protected ActionResult CustomResponse(object result = null)
        {
            return Ok(result);
        }

        protected ActionResult CustomResponse(ErrorOr<BaseResult> result)
        {
            return result.MatchFirst(
                baseResult => Ok(baseResult.Response),
                error => ErrorObjectResult(error)
            );
        }

        private ActionResult ErrorObjectResult(Error error)
        {
            return error.Type switch
            {
                ErrorType.NotFound => NotFound(),
                ErrorType.Validation => new BadRequestObjectResult(new
                {
                    Title = "Bad Request",
                    Status = 400,
                    Errors = new
                    {
                        Messages = error.Description
                    }
                }),
                ErrorType.Conflict => Conflict(),
                _ => BadRequest()
            };
        }
    }
}
