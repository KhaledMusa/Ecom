﻿namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message??(GetmessageromStatusCode(StatusCode));
        }
        private string GetmessageromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unknown Status"
            };
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
