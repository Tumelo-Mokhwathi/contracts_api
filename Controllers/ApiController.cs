using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using contracts_api.Response;

namespace contracts_api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult Error(Result result, string source)
            => ActionResponse.Error(
                System.Net.HttpStatusCode.InternalServerError,
                result.Error,
                source);

        protected IActionResult Ok(object result, string source)
                    => ActionResponse.Success(System.Net.HttpStatusCode.InternalServerError, result, source);

        protected IActionResult OkOrError(Result result, string source)
            => result.IsSuccess ? Ok(source) : Error(result, source);

        protected IActionResult OkOrError<T>(Result<T> result, string source)
            => result.IsSuccess ? Ok(result.Value, source) : Error(result, source);
    }
}
