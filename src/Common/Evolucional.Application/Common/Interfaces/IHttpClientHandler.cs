using System.Threading;
using System.Threading.Tasks;
using Evolucional.Application.Common.Models;
using Evolucional.Domain.Enums;

namespace Evolucional.Application.Common.Interfaces
{
    public interface IHttpClientHandler
    {
        Task<ServiceResult<TResult>> GenericRequest<TRequest, TResult>(string clientApi, string url,
            CancellationToken cancellationToken,
            MethodType method = MethodType.Get,
            TRequest requestEntity = null)
            where TResult : class where TRequest : class;
    }
}