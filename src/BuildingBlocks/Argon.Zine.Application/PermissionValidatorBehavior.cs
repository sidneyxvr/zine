using MediatR;
using System.Reflection;

namespace Argon.Zine.Application;

public class PermissionValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : notnull
{
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;

    public PermissionValidatorBehavior(IRequestHandler<TRequest, TResponse> requestHandler)
    {
        _requestHandler = requestHandler;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var permission = _requestHandler.GetType()
            .GetMethod(nameof(Handle))!
            .GetCustomAttributes<PermissionValidatorAttribute>();

        return await _requestHandler.Handle(request, cancellationToken);
    }
}
