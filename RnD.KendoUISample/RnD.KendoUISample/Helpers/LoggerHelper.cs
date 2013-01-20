using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RnD.KendoUISample.Models;

namespace RnD.KendoUISample.Helpers
{
    public class LoggerHelper
    {
        private AppDbContext _db = new AppDbContext();

        public void LoggerError()
        {
            System.Exception ex = System.Web.HttpContext.Current.Server.GetLastError();
            LoggerError(ex);
        }

        public void LoggerError(Exception ex)
        {
            var currentContext = HttpContext.Current;

            string logSummery, logDetails, logFilePath, logUrl, filePath = "No file path found.", url = "No url found to be reported.";

            if (currentContext != null)
            {
                filePath = currentContext.Request.FilePath;
                url = currentContext.Request.Url.AbsoluteUri;
            }

            logSummery = ex.Message;
            logDetails = ex.ToString();
            logFilePath = filePath;
            logUrl = url;

            // Get Error Type Logger
            LoggerType loggerType = _db.LoggerTypes.SingleOrDefault(x => x.LoggerTypeId == 4);

            if (loggerType != null)
            {
                Logger logger = new Logger() { Summery = logSummery, Details = logDetails, FilePath = logFilePath, Url = logUrl, LoggerTypeId = loggerType.LoggerTypeId };
                //_db.Dispose();
                //_db.Loggers.Add(logger);
                //_db.SaveChanges();
            }
        }
    }
}