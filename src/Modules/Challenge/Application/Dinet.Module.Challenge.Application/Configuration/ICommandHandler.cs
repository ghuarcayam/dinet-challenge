using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinet.Module.Challenge.Application.Contract;

namespace Dinet.Module.Challenge.Application.Configuration
{
    public interface ICommandHandler<in TCommand, TResult> :
       IRequestHandler<TCommand, TResult>
       where TCommand : ICommand<TResult>
    {
    }
}
