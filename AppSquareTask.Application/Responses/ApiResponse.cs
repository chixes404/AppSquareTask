﻿using System.Net;

namespace AppSquareTask.Application.Responses
{
	public class ApiResponse<T> where T : class
	{
		public HttpStatusCode StatusCode { get; set; } 

		public Dictionary<string, object>? Meta { get; set; }

		public bool Succeeded { get; set; }

		public string? Message { get; set; }

		public List<string> Errors { get; set; } = new List<string>();

		public T? Data { get; set; }


		public static ApiResponse<T> Success(T data, string? message = null, Dictionary<string, object>? meta = null) => new ApiResponse<T>
		{
			Data = data,
			Succeeded = true,
			StatusCode = HttpStatusCode.OK,
			Message = message,
			Meta = meta
		};

		// Created response
		public static ApiResponse<T> Created(T data, Dictionary<string, object>? meta = null) => new ApiResponse<T>
		{
			Data = data,
			Succeeded = true,
			StatusCode = HttpStatusCode.Created,
			Meta = meta
		};

		// Error response
		public static ApiResponse<T> Error(HttpStatusCode statusCode, string? message, List<string>? errors = null) => new ApiResponse<T>
		{
			Succeeded = false,
			StatusCode = statusCode,
			Message = message,
			Errors = errors ?? new List<string>()
		};

		// Deleted response
		public static ApiResponse<T> Deleted(string? message = null) => new ApiResponse<T>
		{
			Succeeded = true,
			StatusCode = HttpStatusCode.OK,
			Message = message
		};
	}
}
