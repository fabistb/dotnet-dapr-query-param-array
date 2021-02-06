using System.Threading.Tasks;
using DaprQueryParamArray.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprQueryParamArray.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CallerController : ControllerBase
    {
        private readonly ILogger<CallerController> _logger;
        private readonly IRequestService _requestService;

        public CallerController(ILogger<CallerController> logger, IRequestService requestService)
        {
            _logger = logger;
            _requestService = requestService;
        }
        
        
        [HttpPost("dapr-method-invocation")]
        public async Task<IActionResult> MethodInvocation()
        {
            var response =  await _requestService.DaprMethodInvocation();
            return Ok(response);
        }
        
        [HttpPost("http-client")]
        public async Task<IActionResult> HttpClient()
        {
            var response = await _requestService.HttpInvocation();
            return Ok(response);
        }

        [HttpPost("http-client-dapr")]
        public async Task<IActionResult> HttpClientDapr()
        {
            var response = await _requestService.HttpDaprInvocation();
            return Ok(response);
        }

    }
}