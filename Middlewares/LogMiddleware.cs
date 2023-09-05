//using updateApi.interfaces;

namespace updateApi.Middlewares
{
public class LogMiddleware
{
    private readonly IOLogServices logger;
    private readonly RequestDelegate next;
     public LogMiddleware(RequestDelegate next, IOLogServices logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        logger.Log(LogLevel.Debug, $"start {ctx.Request.Path}");
        await next(ctx);
        logger.Log(LogLevel.Debug, $"end {ctx.Request.Path}");
    }
}
}