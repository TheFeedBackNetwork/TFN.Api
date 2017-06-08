using System;
using Microsoft.AspNetCore.Mvc;
using TFN.Api.Extensions;
using TFN.Domain.Models.ValueObjects;
using TFN.Mvc.Models.Enum;

namespace TFN.Api.Controllers.Base
{
    public class AppController : Controller
    {
        protected Guid UserId => HttpContext.GetUserId().Value;
        protected string Username => HttpContext.GetUsername();
        protected string AbsoluteUri => HttpContext.GetAbsoluteUri();
        protected PrincipleType Caller => HttpContext.GetCaller();
        protected UserAgent UserAgent => HttpContext.GetUserAgent();
    }
}
