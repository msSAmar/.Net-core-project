//using Tasks.Middleware;

namespace updateApi.Middlewares
{
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLogMiddleware(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<LogMiddleware>();

    }


}
}