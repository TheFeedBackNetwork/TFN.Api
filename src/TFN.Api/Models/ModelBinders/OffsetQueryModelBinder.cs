﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TFN.Api.Models.ModelBinders
{
    public class OffsetQueryModelBinder : IModelBinder
    {
        private const string parameterName = "offset";

        private const int minimumValue = 0;

        private const int maximumValue = 65535;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!String.IsNullOrWhiteSpace(bindingContext.ModelName) &&
                bindingContext.ModelName.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase) &&
                bindingContext.ModelType == typeof(int) &&
                bindingContext.ValueProvider.GetValue(bindingContext.ModelName) != null)
            {
                int value;
                var val = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue as string;

                if (String.IsNullOrWhiteSpace(val))
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.FromResult(0);
                }
                else if (int.TryParse(val, out value) && value >= minimumValue && value <= maximumValue)
                {
                    bindingContext.Result = ModelBindingResult.Success(value);
                    return Task.FromResult(0);
                }
                else
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Value is invalid. Offset must be a valid integer between {minimumValue} and {maximumValue}.");
                }
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.FromResult(0);
        }
    }
}
