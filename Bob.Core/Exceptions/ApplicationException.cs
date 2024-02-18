using System.Net;

namespace Bob.Core.Exceptions
{
	public class ApplicationException(string message, HttpStatusCode statusCode) : Exception(message)
	{
		public HttpStatusCode StatusCode { get; set; } = statusCode;
	}

	public class BadRequestException(string message) : ApplicationException(message, HttpStatusCode.BadRequest)
	{
	}

	public class NotFoundException(string message) : ApplicationException(message, HttpStatusCode.NotFound)
	{
	}

	public class SystemException(string message) : ApplicationException(message, HttpStatusCode.InternalServerError)
	{
	}

	public class InvalidOperationException(string message) : ApplicationException(message, HttpStatusCode.Unauthorized)
	{
	}

}