using Grpc.Core.Interceptors;
using Grpc.Core;
using Rira.Application.Common.Exceptions;
using Rira.Domain.Exceptions;

namespace Rira.Presentation.Utilities
{

    public class GrpcExceptionInterceptor : Interceptor
    {
        private readonly IDictionary<Type, Func<Exception, RpcException>> _exceptionHandlers;
        private readonly ILogger<GrpcExceptionInterceptor> _logger;

        public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
        {
            this._logger = logger;
            _exceptionHandlers = new Dictionary<Type, Func<Exception, RpcException>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(EntityNotFoundException), HandleNotFoundException },
                { typeof(Exception), HandleUnknownException },
            };
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                throw HandleException(ex);
            }
        }

        private RpcException HandleException(Exception exception)
        {
            Type type = exception.GetType();
            if (_exceptionHandlers.TryGetValue(type, out var handler))
            {
                return handler.Invoke(exception);
            }

            return HandleUnknownException(exception);
        }

        private RpcException HandleValidationException(Exception exception)
        {
            _logger.LogError("a validation exception happened - message {Message}", exception.Message);

            var validationException = (ValidationException)exception;

            var details = new Metadata();
            foreach (var error in validationException.Errors)
            {
                details.Add($"{error.Key}", string.Join(" | ", error.Value));
            }

            return new RpcException(new Status(StatusCode.InvalidArgument, "Validation failed."), details);
        }

        private RpcException HandleNotFoundException(Exception exception)
        {
            _logger.LogError("a not found exception happened - message {Message}", exception.Message);

            return new RpcException(new Status(StatusCode.NotFound, exception.Message));
        }

        private RpcException HandleUnknownException(Exception exception)
        {
            _logger.LogError("a unkhown exception happened - message {Message}", exception.Message);
            Console.WriteLine($"Unhandled exception: {exception.Message}");
            return new RpcException(new Status(StatusCode.Internal, exception.Message));
        }
    }
}
