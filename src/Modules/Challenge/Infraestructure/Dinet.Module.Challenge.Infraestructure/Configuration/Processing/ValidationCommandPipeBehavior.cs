using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Dinet.Module.Challenge.Infraestructure.Configuration.Processing
{
    internal class ValidationCommandPipeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {

        private readonly IList<IValidator<TRequest>> _validators;

        public ValidationCommandPipeBehavior(IList<IValidator<TRequest>> validators) 
        {
            _validators = validators;

        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
            }

            var result = await next();
            return result;
        }
    }
}
