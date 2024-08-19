using System.Security.Claims;
using AngularNetApi.Core.Exceptions;
using Serilog;

namespace AngularNetApi.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Passa la richiesta al prossimo middleware nella pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";

                if (ex is ServerErrorException serverErrorEx)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} ServerErrorException: {serverErrorEx.Message} , {serverErrorEx.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        serverErrorEx.Message + " " + serverErrorEx.InnerException?.Message
                    );
                }
                // Gestisci eccezioni personalizzate
                else if (ex is NotFoundException notFoundEx)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} NotFoundException: {notFoundEx.Message}  {notFoundEx.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        notFoundEx.Message + " " + notFoundEx.InnerException?.Message
                    );
                }
                else if (ex is BadRequestException exception)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} BadRequestException: {exception.Message}  {exception.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        exception.Message + " " + exception.InnerException?.Message
                    );
                }
                else if (ex is UnauthorizedException unauthorizedEx)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} UnauthorizedException: {unauthorizedEx.Message}  {unauthorizedEx.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        unauthorizedEx.Message + " " + unauthorizedEx.InnerException?.Message
                    );
                }
                else if (ex is LockedOutException lockedOutEx)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} LockedOutException: {lockedOutEx.Message}  {lockedOutEx.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status423Locked;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        lockedOutEx.Message + " " + lockedOutEx.InnerException?.Message
                    );
                }
                else if (ex is ForbiddenException forbiddenEx)
                {
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"UserId: {userId} ForbiddenException: {forbiddenEx.Message}  {forbiddenEx.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        forbiddenEx.Message + " " + forbiddenEx.InnerException?.Message
                    );
                }
                else
                {
                    // Log dell'eccezione generica
                    Log.Error(
                        $"UserId: {userId} Exception: {ex.Message}  {ex.InnerException?.Message}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        ex.Message + " " + ex.InnerException?.Message
                    );
                }
            }
        }
    }
}
