﻿
using Bob.Model;
using System.Net;
using System.Text.Json;

namespace BobAPI.Middleware
{
	public class GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger) : IMiddleware
	{
		private readonly ILogger<GlobalExceptionHandling> _logger = logger;

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				ProblemDetails problem = new()
				{
					Status = (int)HttpStatusCode.InternalServerError,
					Type = "server error",
					Title = "Server error",
					Detail = "An internal server has occured"
				};

				string json = JsonSerializer.Serialize(problem);

				await context.Response.WriteAsync(json);
				context.Response.ContentType = "application/json";
			}
		}
	}
}
