﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TFN.Sts.Extensions;

namespace TFN.Sts.UI.Base
{
    // ReSharper disable once InconsistentNaming
    public class UIController : Controller
    {
        public string AppUrl => HttpContext.GetAppUrl();
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            /*if(!context.HttpContext.Request.PathBase.Equals($"/{RoutePaths.IdentityRootBase}"))
            {
                context.Result = NotFound();
            }*/
            
        }
    }
}