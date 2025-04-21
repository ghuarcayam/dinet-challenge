using Autofac;
using MediatR;
using Dinet.Module.Challenge.Application.Contract;
using Dinet.Module.Challenge.Infraestructure.Configuration;

namespace Dinet.Module.Challenge.Infraestructure
{
    public class ChallengeModule : IChallengeModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        {
            using (var scope = ChallengeCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(command, cancellationToken);
            }
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            using (var scope = ChallengeCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query, cancellationToken);
            }
        }
    }
}
