using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaprQueryParamArray.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaprQueryParamArray.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalleeController : ControllerBase
    {
        private readonly ILogger<CallerController> _logger;

        public CalleeController(ILogger<CallerController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetArrayQuery ([FromQuery] string[] name)
        {
            var responseDto = new ResponseDto {ReceivedNames = new List<string>()};
            responseDto.ReceivedNames.AddRange(name);

            return Ok(responseDto);
        }
    }
}