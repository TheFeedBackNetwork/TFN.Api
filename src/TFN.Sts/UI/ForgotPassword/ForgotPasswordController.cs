using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TFN.Sts.UI.Base;
using TFN.Domain.Interfaces.Services;

namespace TFN.Sts.UI.ForgotPassword
{
    public class ForgotPasswordController : UIController
    {
        public IUserService UserService { get; private set; }
        public ForgotPasswordController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet("forgotpassword", Name = "ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost("forgotpassword", Name = "ForgotPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new ForgotPasswordViewModel());
            }

            var user = await UserService.FindByEmail(model.ForgotPasswordEmail);
            if (user != null)
            {
                await UserService.SendChangePasswordKey(user);
            }

            return RedirectToAction("ForgotPasswordSuccess");
        }

        [HttpGet("forgotpassword/success", Name = "ForgotPasswordSuccess")]
        public IActionResult ForgotPasswordSuccess()
        {
            return View("ForgotPasswordSuccess");
        }
    }
}
