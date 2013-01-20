using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RnD.KendoUISample.Helpers
{
    public static class ExceptionHelper
    {
        public static string ExceptionMessageFormat(Exception ex, bool log = false)
        {
            string message = "Error: There was a problem while processing your request: " + ex.Message;

            if (ex.InnerException != null)
            {
                Exception inner = ex.InnerException;
                if (inner is System.Data.Common.DbException)
                    message = "Database is currently experiencing problems. " + inner.Message;
                else if (inner is System.Data.UpdateException)
                    message = "Datebase is currently updating problem.";
                else if (inner is System.Data.EntityException)
                    message = "Entity is problem.";
                else if (inner is NullReferenceException)
                    message = "There are one or more required fields that are missing.";
                else if (inner is ArgumentException)
                {
                    string paramName = ((ArgumentException)inner).ParamName;
                    message = string.Concat("The ", paramName, " value is illegal.");
                }
                else if (inner is ApplicationException)
                    message = "Exception in application" + inner.Message;
                else
                    message = inner.Message;

            }

            if (log)
            {
                LoggerHelper loggerHelper = new LoggerHelper();
                loggerHelper.LoggerError(ex);
            }

            return message;
        }

        public static string ModelStateErrorFormat(System.Web.Mvc.ModelStateDictionary modelStateDictionary)
        {
            string message = @"<div class='mess'>";

            foreach (var modelStateValues in modelStateDictionary.Values)
            {
                if (modelStateValues.Errors.Any())
                {
                    foreach (var modelError in modelStateValues.Errors)
                    {
                        message += "<p>";
                        message += modelError.ErrorMessage;
                        message += "</p>";
                    }
                }
            }

            message += "</div>";

            return message;
        }
    }
}