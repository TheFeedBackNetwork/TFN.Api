﻿using Microsoft.AspNetCore.Mvc;

namespace TFN.Sts.UI.TermsConditions
{
    public class TermsConditionsController : Controller
    {
        [HttpGet]
        [Route("terms-conditions", Name = "TermsConditions")]
        public IActionResult TermsConditions()
        {
            return View();
        }
    }
}