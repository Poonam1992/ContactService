using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;
using ContactServiceSolution.Service.CustomException;
using Newtonsoft.Json;

namespace ContactServiceSolution.API.ExceptionMiddleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate nextRequest;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.nextRequest = next;

        }

        public async Task Invoke (HttpContext context)
        {
            try
            {
                await nextRequest(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context,Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; 
            if (ex is ContactNotFoundException) code = HttpStatusCode.NotFound;
           

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            //We can add logger here to log errors. Now Just writing error to Output window.
            System.Diagnostics.Debug.WriteLine($"The following Exception Occured: {ex.Message}");

            return context.Response.WriteAsync(result);
        }

    }
}
