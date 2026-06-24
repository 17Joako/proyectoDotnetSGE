using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace SGE.WebApi;

public class ManejadorDeExceptionsGlobales : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails { Instance = httpContext.Request.Path };

        if (exception is DominioException)
        {
            problemDetails.Title = "Error de dominio";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Detail = exception.Message;
        }
        else if (exception is AutorizacionException)
        {
            problemDetails.Title = "Error de autorización";
            problemDetails.Status = StatusCodes.Status403Forbidden;
            problemDetails.Detail = exception.Message;
        }
        else if (exception is entidadNoEncontradaException)
        {
            problemDetails.Title = "Entidad no encontrada";
            problemDetails.Status = StatusCodes.Status404NotFound;
            problemDetails.Detail = exception.Message;
        }
        else
        {
            problemDetails.Title = "Error interno del servidor";
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Detail = exception.Message;
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}