using MediatR;
using timeZZle.Shared.Utils;

namespace timeZZle.Shared.Interfaces.Messaging;

public interface IQueryHandler<in TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>;
