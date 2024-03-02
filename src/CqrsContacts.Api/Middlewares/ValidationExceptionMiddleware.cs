namespace CqrsContacts.Api.Middlewares
{
    using FluentValidation;

    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                if (ex.Errors.Count() > 0)
                {
                    await context.Response.WriteAsJsonAsync(ex.Errors.Select(x => x.ErrorMessage));
                }
                else
                {
                    await context.Response.WriteAsJsonAsync(new[] { ex.Message });
                }
            }
        }
    }
}
