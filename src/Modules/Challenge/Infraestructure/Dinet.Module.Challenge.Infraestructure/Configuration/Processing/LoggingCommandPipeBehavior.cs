using MediatR;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Processing
{
    internal class LoggingCommandPipeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly Serilog.ILogger _logger;

        private readonly IExecutionContextAccessor _executionContextAccessor;

        public LoggingCommandPipeBehavior(
           Serilog.ILogger logger,
           IExecutionContextAccessor executionContextAccessor)
        {
            _logger = logger;
            _executionContextAccessor = executionContextAccessor;

        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            
            using (
                LogContext.Push(
                    new RequestLogEnricher(_executionContextAccessor),
                    new CommandLogEnricher(request)))
            {
                try
                {
                    _logger.Information(
                       "Executing command {Command}",
                       request.GetType().Name);

                    var result = await next();

                    this._logger.Information("Command {Command} processed successful", request.GetType().Name);

                    return result;
                }
                catch (Exception exception)
                {
                    this._logger.Error(exception, "Command {Command} processing failed", request.GetType().Name);

                    throw;
                }
            }

            
        }
        private class CommandLogEnricher : ILogEventEnricher
        {
            private readonly ICommand<TResponse> _command;

            public CommandLogEnricher(ICommand<TResponse> command)
            {
                _command = command;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.GetType().Name}")));
            }
        }

        private class RequestLogEnricher : ILogEventEnricher
        {
            private readonly IExecutionContextAccessor _executionContextAccessor;

            public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
            {
                _executionContextAccessor = executionContextAccessor;
            }

            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (_executionContextAccessor.IsAvailable)
                {
                    logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(_executionContextAccessor.CorrelationId)));
                }
            }
        }
    }

}
