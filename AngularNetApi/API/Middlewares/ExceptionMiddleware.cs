using AngularNetApi.Core.Exceptions;

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
                if (ex is ServerErrorException serverErrorEx)
                {
                    // Log dell'eccezione specifica
                    //Log.Error(serverErrorException, "ServerErrorException: {Message}", serverErrorException.Message);

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
                    //Log.Error(notFoundException, "NotFoundException: {Message}", notFoundException.Message);

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
                    //Log.Error(validationException, "ValidationException: {Message}", validationException.Message);

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
                    //Log.Error(unauthorizedAccessException, "UnauthorizedAccessException: {Message}", unauthorizedAccessException.Message);

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
                    //Log.Error(lockedOutException, "LockedOutException: {Message}", lockedOutException.Message);

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
                    //Log.Error(forbiddenException, "ForbiddenException: {Message}", forbiddenException.Message);

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
                    //Log.Error(ex, "Si è verificato un errore durante l'elaborazione della richiesta.");

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
