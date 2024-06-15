﻿using System;
using System.Net;

namespace Common.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ApiResultStatusCode ApiStatusCode { get; set; }
        public object AdditionalData { get; set; }
        public AppException(ApiResultStatusCode statusCode)
            : this(statusCode, null)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message)
            : this(statusCode, message, HttpStatusCode.InternalServerError)
        {
        }
        public AppException(ApiResultStatusCode statusCode, object additionalData)
            : this(statusCode, null, additionalData)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
            : this(statusCode, message, httpStatusCode, null)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
            : this(statusCode, message, httpStatusCode, null, additionalData)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, Exception exception)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, Exception exception, object additionalData)
            : this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
            : this(statusCode, message, httpStatusCode, exception, null)
        {
        }
        public AppException(ApiResultStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
            : base(message, exception)
        {
            ApiStatusCode = statusCode;
            HttpStatusCode = httpStatusCode;
            AdditionalData = additionalData;
        }
    }
}
