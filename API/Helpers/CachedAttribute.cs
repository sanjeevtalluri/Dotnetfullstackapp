using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreData.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {
            this.timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IresponseCacheService>();
            var cacheKey = GenerateCacheKeyFromContext(context.HttpContext.Request);
            var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }

            var executedContext = await next();
            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(cacheKey,okObjectResult.Value,TimeSpan.FromSeconds(timeToLiveSeconds));
            }
        }

        private string GenerateCacheKeyFromContext(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}