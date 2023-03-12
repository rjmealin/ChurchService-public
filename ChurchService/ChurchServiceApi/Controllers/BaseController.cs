using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Extensions;
using DB = ChurchService.Library.Models;
using Microsoft.AspNetCore.Http;
using ChurchServiceApi.Security;

namespace ChurchServiceApi.Controllers
{
    public class BaseController : ControllerBase
    {
        internal JsonWebToken _token;












        /// <summary>
        ///     Returns the ip address of the client machine that originated the request.
        /// </summary>
        /// 
        internal string GetUserIpAddress()
        {

            var conn = HttpContext.Connection;

            var ip = conn.RemoteIpAddress;

            if (ip != null)
                return ip.ToString();
            else
                return "";
        }










        /// <summary>
        ///     Logs the given exception.
        /// </summary>
        /// 
        /// <param name="ex">The exception object to log</param>
        /// <param name="viewModel">The model/value, if applicable (pass null if not), for the request.</param>
        /// 
        internal void LogException(Exception ex, object viewModel = null)
        {
            try
            {

                DB.Logging.Errors.LogError(
                    ex,
                    GetUserIpAddress(),
                    CurrentJsonWebToken.UserId,
                    Request.GetDisplayUrl(),
                    viewModel);
            }
            catch (Exception) { } // Prevent run-away exception logging loop.
        }










        /// <summary>
        ///     Gets the JSON web token for this request.
        /// </summary>
        /// 
        internal JsonWebToken CurrentJsonWebToken
        {
            get
            {

                var headerString = Request.Headers.Authorization;

                var authHeader = new System.Net.Http.Headers.AuthenticationHeaderValue(headerString);

                if (_token == null)
                    _token = new JsonWebToken(authHeader);

                return _token;
            }
        }










        /// <summary>
        ///     Returns an action result with status code 403 and the WWW-Authenticate header.
        /// </summary>
        /// 
        /// <param name="message"></param>
        /// 
        internal IActionResult ApiUnauthorized(string message = "Unauthorized access!")
        {
            var response = new ContentResult()
            {
                Content = message,
                ContentType = "text/plain",
                StatusCode = 401
            };

            return response;

        }










        /// <summary>
        ///     Returns an action result with status code 200 and the correct 
        ///     content/headers to download a file.
        /// </summary>
        /// 
        /// <param name="fileData">The binary file data.</param>
        /// <param name="fileName">The filename of the object.</param>
        /// <param name="mime">The MIME/Content type of the file.</param>
        /// 
        internal IActionResult FileResult(byte[] fileData, string fileName, string mime)
        {
            //var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new ByteArrayContent(fileData) };

            //responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = fileName };
            //responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mime);


            //return new System.Web.Http.Results.ResponseMessageResult(responseMessage);

            return FileResult(fileData, fileName, mime);
        }










        /// <summary>
        ///     Returns an action result with status code 449 with the auth 
        ///     type and conditionally the auth url headers.
        /// </summary>
        /// 
        /// <param name="message">The error message.</param>
        /// <param name="authType">The authorization type.</param>
        /// <param name="authUrl">The authorization url.</param>
        /// 
        internal IActionResult ApiAuthorizationRequired(string message, string authType, string authUrl = null)
        {
            var responseMessage = new System.Net.Http.HttpResponseMessage((HttpStatusCode)449); // Retry With (Microsoft).  The request should be retried after performing the action described in the response.
            var exposedHeaders = "AuthType";
            responseMessage.Headers.Add("AuthType", authType);
            if (!string.IsNullOrWhiteSpace(authUrl))
            {
                responseMessage.Headers.Add("URL-Authenticate", authUrl);
                exposedHeaders += ", URL-Authenticate";
            }
            responseMessage.Headers.Add("Access-Control-Expose-Headers", exposedHeaders);
            responseMessage.Content = new StringContent(message, Encoding.UTF8, "text/html");

            return StatusCode(449, responseMessage);
        }









        /// <summary>
        ///     Returns an action result appropriate for the given exception type.
        /// </summary>
        /// 
        /// <param name="ex">Exception</param>
        /// 
        internal IActionResult ReturnException(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
                return ApiUnauthorized("Invalid username and/or password.");
            else if (ex is KeyNotFoundException)
                return NotFound();
            else
            {
                var msg = string.Empty;
                while (ex != null)
                {
                    if (msg.Length > 0)
                        msg += Environment.NewLine;
                    msg += ex.Message;
                    ex = ex.InnerException;
                }

                var result = StatusCode(StatusCodes.Status500InternalServerError, msg);
                return result;

            }
        }



















        /// <summary>
        ///     Returns the No Content (204) status.
        /// </summary>
        /// 
        internal IActionResult NoContent()
        {

            var result = StatusCode(StatusCodes.Status204NoContent);
            
            return result;
            
        }
    }
}
