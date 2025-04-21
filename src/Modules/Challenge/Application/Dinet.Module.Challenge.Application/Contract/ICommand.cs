using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinet.Module.Challenge.Application.Contract
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
