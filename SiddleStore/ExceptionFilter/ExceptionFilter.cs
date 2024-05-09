using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiddleStore.Models;

namespace SiddleStore.ExceptionFilterHandeling
{
    public class ExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            this._logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is InvalidOperationException)
            {
                var error = new ErrorModel
            (
                400,
                "Invalid input!"

            );
                _logger.LogInformation("erroMsg: " + context.Exception.Message);
                context.Result = new JsonResult(error);
                _logger.LogInformation("stackTrace: " + context.Exception.StackTrace?.ToString());
                context.Result = new JsonResult(error);
            }
            else
            {
                var error = new ErrorModel
            (
                500,
                "Server erro!"
            );
                _logger.LogInformation("erroMsg: " + context.Exception.Message);
                context.Result = new JsonResult(error);
                _logger.LogInformation("stackTrace: " + context.Exception.StackTrace?.ToString());
                context.Result = new JsonResult(error);
            }
        }
    }
}
