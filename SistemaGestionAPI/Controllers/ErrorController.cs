using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SistemaGestionAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context =
                HttpContext.Features
                .Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.Message,
                title: "Ha ocurrido un error");
        }
    }
}