﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration["ApiKeys:StandardApiKey"];

        if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var key))
        {
            context.Result = new UnauthorizedObjectResult(new { success = false, error = "Invalid api-key or api-key is missing." });
            // return Task.CompletedTask;
            return;
        }

        if (string.IsNullOrWhiteSpace(apiKey) || !string.Equals(key, apiKey))
        {
            context.Result = new UnauthorizedObjectResult(new { success = false, error = "Invalid api-key." });
            // return Task.CompletedTask;
            return;
        };

        await next();
    }
}
