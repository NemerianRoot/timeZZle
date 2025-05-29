using MediatR;
using timeZZle.Shared.Utils;

namespace timeZZle.Shared.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;