using Bob.Core;
using Bob.Model;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using ApplicationException = Bob.Core.Exceptions.ApplicationException;

namespace BobAPI.Middleware
{
	public class ExceptionMiddlewareExtension
	{
		private readonly ILogger<ExceptionMiddlewareExtension> _logger;

		public ExceptionMiddlewareExtension(ILogger<ExceptionMiddlewareExtension> logger)
		{
			_logger = logger;
		}
		public void ConfigureExceptionHandler(IApplicationBuilder app)
		{
			app.UseExceptionHandler(error =>
			{

				error.Run(async context =>
				{
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

					context.Response.ContentType = "application/json";
					
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is ApplicationException exception)
                        {
							_logger.LogError(contextFeature.Error.Message);
							context.Response.StatusCode = (int)exception.StatusCode;
							await context.Response.WriteAsync(JsonConvert.SerializeObject( new APIResponse<string>
							{
								IsSuccess = false,
								Message = contextFeature.Error.Message
							}));
                        }
                        else
                        {
							_logger.LogError(contextFeature.Error.Message);	
							context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
							await context.Response.WriteAsync(JsonConvert.SerializeObject(new APIResponse<string>
							{
								IsSuccess = false,
								Message = ResponseMessage.IsError
							}));
						}
                    }
				});
			});
		}
	}
}
