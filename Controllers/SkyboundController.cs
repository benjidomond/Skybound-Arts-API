using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace buttoncheckDevAPI.Controllers
{
    //Convention
    [Route("api/[controller]")]
    [ApiController]
    public class SkyboundController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "this", "is", "hard", "coded" };
        }
    }
}
