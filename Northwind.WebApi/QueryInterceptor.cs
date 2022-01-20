using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DiagnosticAdapter;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;

namespace Northwind.WebApi
{
    public class QueryInterceptor
    {
        private readonly ILogger<QueryInterceptor> _logger;
        private string _query;
        private DateTimeOffset _startTime;
        public QueryInterceptor(ILogger<QueryInterceptor> logger)
        {
            _logger = logger;
        }

        [DiagnosticName("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting")]
        public void OnCommandExecuting(DbCommand command, DbCommandMethod executeMethod, Guid commandId, Guid connectionId, bool async, DateTimeOffset startTime)
        {
            _query = command.CommandText;
            _startTime = startTime;
        }

        [DiagnosticName("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuted")]
        public void OnCommandExecuted(object result, bool async)
        {
            var endTime = DateTimeOffset.Now;
            var queryTiming = (endTime - _startTime).TotalSeconds;
            _logger.LogInformation("\n" + "Executes " + "\n" + _query + "\n" + "in " + queryTiming + " seconds\n");
        }

        [DiagnosticName("Microsoft.EntityFrameworkCore.Database.Command.CommandError")]
        public void OnCommandError(Exception exception, bool async)
        {
            _logger.LogError(exception.Message);
        }
    }
}