using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BigDataAssigment.Api.Helpers
{
    public class HttpResponseHelper
    {
        
        
        public static IActionResult ValidationErrorResult(string title = "Validation Error", int status = 400, string detail = null, string instance = null) =>
            new BadRequestObjectResult(
                new ValidationProblemDetails
                {
                    Title = title,
                    Status = status,
                    Detail = detail,
                    Instance = instance
                })
            { StatusCode = (int)HttpStatusCode.BadRequest };
    }
}
