using System.Net;

namespace AppSquareTask.Core.Responses
{
	public class ApiResponseHandler
	{
		// Constructor kept protected for inheritance
		public ApiResponseHandler() { }

		// Reusable private method for error responses
		private ApiResponse<T> CreateErrorResponse<T>(HttpStatusCode statusCode, string? message) where T : class
		{
			return ApiResponse<T>.Error(statusCode, message);
		}

		// Error responses
		public ApiResponse<T> BadRequest<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.BadRequest, message);
		}

		public ApiResponse<T> Unauthorized<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.Unauthorized, message);
		}

		public ApiResponse<T> NotFound<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.NotFound, message);
		}

		public ApiResponse<T> UnprocessableEntity<T>(string? message = null) where T : class
		{
			return CreateErrorResponse<T>(HttpStatusCode.UnprocessableEntity, message);
		}

		// This method will handle success with no data (returning `null` for data)
		public ApiResponse<T> Success<T>(string? message = null) where T : class
		{
			return ApiResponse<T>.Success(null, message);
		}


		// Success with entity data
		public ApiResponse<T> Success<T>(T entity, string? message = null, Dictionary<string, object>? meta = null) where T : class
		{
			return ApiResponse<T>.Success(entity, message, meta);
		}

		public ApiResponse<T> Created<T>(T entity, Dictionary<string, object>? meta = null) where T : class
		{
			return ApiResponse<T>.Created(entity, meta);
		}

		public ApiResponse<T> Deleted<T>(string? message = null) where T : class
		{
			return ApiResponse<T>.Deleted(message);
		}
	}
}
