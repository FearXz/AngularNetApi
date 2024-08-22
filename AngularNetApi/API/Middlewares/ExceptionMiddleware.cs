using AngularNetApi.Core.Exceptions;
using Serilog;
using System.Security.Claims;

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
                var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

                if (ex is ServerErrorException serverErrorEx)
                {
                    var methodName = serverErrorEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {serverErrorEx.Message} {serverErrorEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {serverErrorEx.Message} {serverErrorEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                // Gestisci eccezioni personalizzate
                else if (ex is NotFoundException notFoundEx)
                {
                    var methodName = notFoundEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {notFoundEx.Message} {notFoundEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {notFoundEx.Message} {notFoundEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                else if (ex is BadRequestException badRequestEx)
                {
                    var methodName = badRequestEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);

                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {badRequestEx.Message} {badRequestEx.InnerException?.Message} \n\n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {badRequestEx.Message} {badRequestEx.InnerException?.Message} \n\n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                else if (ex is UnauthorizedException unauthorizedEx)
                {
                    var methodName = unauthorizedEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {unauthorizedEx.Message} {unauthorizedEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {unauthorizedEx.Message} {unauthorizedEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                else if (ex is LockedOutException lockedOutEx)
                {
                    var methodName = lockedOutEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {lockedOutEx.Message} {lockedOutEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status423Locked;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {lockedOutEx.Message} {lockedOutEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                else if (ex is ForbiddenException forbiddenEx)
                {
                    var methodName = forbiddenEx.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione specifica
                    Log.Error(
                        $"Exception caught in {methodName}: {forbiddenEx.Message} {forbiddenEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {forbiddenEx.Message} {forbiddenEx.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
                else
                {
                    var methodName = ex.TargetSite?.DeclaringType?.Name;
                    if (methodName != null && methodName.Contains('<'))
                        methodName = methodName.Substring(1, methodName.IndexOf('>') - 1);
                    // Log dell'eccezione generica
                    Log.Error(
                        $"Exception caught in {methodName}: {ex.Message} {ex.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );

                    // Imposta il codice di stato e il contenuto della risposta
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "text/plain";
                    await context.Response.WriteAsync(
                        $"Exception caught in {methodName}: {ex.Message} {ex.InnerException?.Message} \n{(string.IsNullOrEmpty(userId) ? "" : "UserId: " + userId)}"
                    );
                }
            }
        }
    }
}
