namespace NutriPlate.Api.Middleware;
public class ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger) {
 public async Task Invoke(HttpContext context){try{await next(context);}catch(Exception ex){logger.LogError(ex,"Unhandled request error");context.Response.ContentType="application/json";context.Response.StatusCode=ex switch{KeyNotFoundException=>404,InvalidOperationException=>409,ArgumentException=>400,_=>500};var message=context.Response.StatusCode==500?"An unexpected error occurred.":ex.Message;await context.Response.WriteAsJsonAsync(new{error=message,status=context.Response.StatusCode});}}
}
