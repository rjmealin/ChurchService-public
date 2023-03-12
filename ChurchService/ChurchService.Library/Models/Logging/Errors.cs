using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChurchService.Library;

namespace ChurchService.Library.Models.Logging
{
    public static class Errors
    {
        /// <summary>
        ///     Logs the exception and any inner exceptions.
        /// </summary>
        /// 
        /// <param name="websiteId">The database id of the website that owns this resource.</param>
        /// <param name="ex">The exception object to log.</param>
        /// <param name="userIpAddress">The ip address of the client machine.</param>
        /// <param name="userId">The user id of the current user, if applicable.</param>
        /// <param name="url">The url of the request, if applicable.</param>
        /// <param name="viewModel">The view model of the request, if applicable.</param>
        /// 
        public static void LogError(Exception ex, string userIpAddress, Guid userId, string url = null, object viewModel = null)
        {
            string json = null;

            if (viewModel != null)
                json = Newtonsoft.Json.JsonConvert.SerializeObject(viewModel);

            RecursiveLogError(ex, null, userIpAddress, userId, url, json);
        }










        /// <summary>
        ///     Recursively logs the exception and any inner exceptions.
        /// </summary>
        /// 
        /// <param name="ex">The exception object to log.</param>
        /// <param name="parentErrorLogId">The parent exception.</param>
        /// <param name="userIpAddress">The ip address of the client machine.</param>
        /// <param name="userId">The user id of the current user, if applicable.</param>
        /// <param name="url">The url of the request, if applicable.</param>
        /// <param name="viewModel">The view model of the request, if applicable.</param>
        /// 
        private static void RecursiveLogError(Exception ex, Guid? parentErrorLogId, string userIpAddress, Guid userId, string url, string viewModelJson)
        {
            Guid? errorLogId = null;

            using (var db = new SQL.ChurchServiceContext())
            {
                var log = new SQL.ErrorLog();

                log.ParentErrorLogId = parentErrorLogId;
                log.ExceptionMessage = ex.Message;
                log.StackTrace = ex.StackTrace;
                log.TimeStamp = DateTime.Now;
                log.UserId = userId;
                log.UserIpAddress = userIpAddress;
                log.ViewModel = viewModelJson;


                if (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    var eve = ex as Microsoft.EntityFrameworkCore.DbUpdateException;
                    var msg = log.ExceptionMessage;

                    if (eve.Message != null)
                        msg += eve.Message;


                    log.ExceptionMessage = msg;
                }

                db.ErrorLogs.Add(log);
                db.SaveChanges();

                errorLogId = log.ErrorLogId;
            }

            if (ex.InnerException != null)
                RecursiveLogError(ex.InnerException, errorLogId, userIpAddress, userId, null, null);
        }










        /// <summary>
        ///     Logs a non-exception error.
        /// </summary>
        /// 
        /// <param name="websiteId">The database id of the website that owns this resource.</param>
        /// <param name="message">Details about the error.</param>
        /// <param name="location">The location the error occured.</param>
        /// <param name="userIpAddress">The ip address of the client machine.</param>
        /// <param name="userId">The user id of the current user, if applicable.</param>
        /// <param name="url">The url of the request, if applicable.</param>
        /// <param name="viewModel">The view model of the request, if applicable.</param>
        /// 
        public static void LogError(string message, string location, string userIpAddress, Guid userId, string url, string viewModelJson)
        {
            using (var db = new SQL.ChurchServiceContext())
            {
                var log = new SQL.ErrorLog();

                log.ParentErrorLogId = null;
                log.ExceptionMessage = message;
                log.StackTrace = string.IsNullOrWhiteSpace(location) ? string.Empty : location;
                log.TimeStamp = DateTime.Now;
                log.UserId = userId;
                log.UserIpAddress = userIpAddress;
                log.ViewModel = viewModelJson;

                db.ErrorLogs.Add(log);
                db.SaveChanges();

            }
        }
    }
}
