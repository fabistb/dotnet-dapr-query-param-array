using System.Threading.Tasks;
using DaprQueryParamArray.Models;

namespace DaprQueryParamArray.Services
{
    public interface IRequestService
    {
        Task<ResponseDto> DaprMethodInvocation();

        Task<ResponseDto> HttpInvocation();

        Task<ResponseDto> HttpDaprInvocation();
    }
}