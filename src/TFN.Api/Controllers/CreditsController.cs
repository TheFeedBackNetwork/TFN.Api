using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Authorization.Models.Resource;
using TFN.Api.Authorization.Operations;
using TFN.Api.Controllers.Base;
using TFN.Api.Models.Interfaces;
using TFN.Domain.Interfaces.Services;
using TFN.Mvc.HttpResults;

namespace TFN.Api.Controllers
{
    [Route("credits")]
    public class CreditsController : AppController
    {
        public ICreditService CreditService { get; private set; }
        public ICreditsResponseModelFactory CreditsResponseModelFactory { get; private set; }
        public IAuthorizationService AuthorizationService { get; private set; }
        public CreditsController(ICreditService creditService, IAuthorizationService authorizationService, ICreditsResponseModelFactory creditsResponseModelFactory)
        {
            CreditService = creditService;
            CreditsResponseModelFactory = creditsResponseModelFactory;
            AuthorizationService = authorizationService;
        }

        [HttpGet(Name = "GetCreditsForCaller")]
        [Authorize("credits.read")]
        public async Task<IActionResult> GetCredits()
        {
            var credit = await CreditService.FindByUserId(UserId);

            if (credit == null)
            {
                return NotFound();
            }

            var authZModel = CreditsAuthorizationModel.From(credit);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CreditsOperations.Read))
            {
                return new HttpForbiddenResult("An attempt to read credits was attempted, but the authorization policy challenged the request");
            }

            var model = CreditsResponseModelFactory.From(credit, AbsoluteUri);

            return Json(model);
        }

        [HttpGet("{creditId:Guid}", Name = "GetCredits")]
        [Authorize("credits.read")]
        public async Task<IActionResult> GetCredits(Guid creditId)
        {
            var credit = await CreditService.Find(creditId);

            if (credit == null)
            {
                return NotFound();
            }

            var authZModel = CreditsAuthorizationModel.From(credit);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CreditsOperations.Read))
            {
                return new HttpForbiddenResult("An attempt to read credits was attempted, but the authorization policy challenged the request");
            }

            var model = CreditsResponseModelFactory.From(credit, AbsoluteUri);

            return Json(model);
        }

        [HttpGet("users/{userId:Guid}", Name = "GetCreditsByUserId")]
        [Authorize("credits.read")]
        public async Task<IActionResult> GetCreditsByUserId(Guid userId)
        {
            var credit = await CreditService.FindByUserId(userId);

            if (credit == null)
            {
                return NotFound();
            }

            var authZModel = CreditsAuthorizationModel.From(credit);

            if (!await AuthorizationService.AuthorizeAsync(User, authZModel, CreditsOperations.Read))
            {
                return new HttpForbiddenResult("An attempt to read credits was attempted, but the authorization policy challenged the request");
            }

            var model = CreditsResponseModelFactory.From(credit, AbsoluteUri);

            return Json(model);
        }

    }
}