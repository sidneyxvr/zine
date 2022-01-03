using MediatR;
using System.Reflection;

namespace Argon.Zine.Application;

public class PermissionValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IRequestHandler<TRequest, TResponse> requestHandler;

    public PermissionValidatorBehavior(IRequestHandler<TRequest, TResponse> requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var permission = requestHandler.GetType()
            .GetMethod(nameof(Handle))!
            .GetCustomAttributes<PermissionValidatorAttribute>();

        return await requestHandler.Handle(request, cancellationToken);
    }
}
