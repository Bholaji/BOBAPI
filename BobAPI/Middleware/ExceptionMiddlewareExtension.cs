using Bob.Core;
using Bob.Model;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using ApplicationException = Bob.Core.Exceptions.ApplicationException;

namespace BobAPI.Middleware
{
	public static class ExceptionMiddlewareExtension
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app)
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
							context.Response.StatusCode = (int)exception.StatusCode;
							await context.Response.WriteAsync(JsonConvert.SerializeObject( new APIResponse<string>
							{
								IsSuccess = false,
								Message = contextFeature.Error.Message
							}));
                        }
                        else
                        {
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
