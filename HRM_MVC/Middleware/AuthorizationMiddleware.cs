using HRM_MVC.SessionManager;

namespace Prn221_group_project.Middleware
{
    public class CustomAuthorizationMiddleWare
    {
        private readonly RequestDelegate _next;
        public CustomAuthorizationMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            LoginAccount? loginAccount = SessionHelper.GetObjectFromSession<LoginAccount>(context.Session, KeyConstants.ACCOUNT_KEY);
            if(loginAccount != null)
            {
                Console.WriteLine("OK");
                await _next(context);

            }
            else
            {
                if (context.Request.Path.Value.Contains("Login"))
                {
                    await _next(context);
                }
                else
                {
                    context.Response.Redirect("Login");
                }
            }
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthorization(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorizationMiddleWare>();
        }
    }
}
