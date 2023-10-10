using Serilog;
using MediatR;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_WebApi.Application.Common.Behaivors
{
    public class LoggingBehaivor<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest 
        : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            Log.Information("Request: {@Name} {@Request}", 
                requestName, request);

            var response = await next();

            return response;
        }
    }
}
