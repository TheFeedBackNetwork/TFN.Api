using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Controllers.Base;
using TFN.Api.Extensions;
using TFN.Api.Models.Interfaces;
using TFN.Api.Models.ModelBinders;
using TFN.Api.Models.ResponseModels;
using TFN.Domain.Interfaces.Services;

namespace TFN.Api.Controllers
{

    [Route("users")]
    public class UsersController : AppController
    {
        public IUserService UserService { get; private set; }
        public ICreditsResponseModelFactory CreditsResponseModelFactory { get; private set; }
        public IUsersResponseModelFactory UsersResponseModelFactory { get; private set; }
        public UsersController(IUserService userService, ICreditsResponseModelFactory creditsResponseModelFactory, IUsersResponseModelFactory usersResponseModelFactory)
        {
            UserService = userService;
            CreditsResponseModelFactory = creditsResponseModelFactory;
            UsersResponseModelFactory = usersResponseModelFactory;
        }

        [HttpGet(Name = "SearchUsers")]
        [Authorize("users.read")]
        public async Task<IActionResult> SearchUsers(
            [ModelBinder(BinderType = typeof(UsernameQueryModelBinder))] string username,
            [ModelBinder(BinderType = typeof(OffsetQueryModelBinder))]short offset = 0,
            [ModelBinder(BinderType = typeof(LimitQueryModelBinder))]short limit = 7)
        {
            
            var users = await UserService.SearchUsers(username, offset, limit);

            var model = users.Select(x => CreditsResponseModelFactory.From(x, HttpContext.GetAbsoluteUri()));

            return Json(model);
        }

        [HttpGet("me",Name = "SearchMe")]
        [Authorize("users.read")]
        public async Task<IActionResult> SearchMe()
        {
            var user = await UserService.GetByUsernameAsync(HttpContext.GetUsername());

            if (user == null)
            {
                return NotFound();
            }

            var model = await UsersResponseModelFactory.From(user, AbsoluteUri);
            
            return Json(model);
        }
    }
}