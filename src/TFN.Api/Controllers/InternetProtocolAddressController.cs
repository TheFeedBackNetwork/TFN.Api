using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Controllers.Base;

namespace TFN.Api.Controllers
{
    [Route("ip")]
    public class InternetProtocolAddressController : AppController
    {
        [HttpGet(Name = "GetIPForCaller")]
        [Authorize("ip.read")]
        public IActionResult GetIPAddress()
        {
            var IP = HttpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var model = new Dictionary<string, string> {["IP"] = IP};

            return Json(model);
        }
    }
}