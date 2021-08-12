using DotNetAPI.Core;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    { 
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null) return NotFound();
            if(result.IsSuccess && result.Value != null) return Ok(result.Value);
            if(result.IsSuccess && result.Value == null) return NotFound();
            
            return BadRequest(result.Error);
        }
    }
}