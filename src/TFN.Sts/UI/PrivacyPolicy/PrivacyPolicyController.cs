using Microsoft.AspNetCore.Mvc;

namespace TFN.Sts.UI.PrivacyPolicy
{
    public class PrivacyPolicyController : Controller
    {
        [HttpGet]
        [Route("privacy-policy", Name = "PrivacyPolicy")]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}
