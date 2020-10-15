using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementSystemAPI.Classes
{
    public class ValidateModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var modelErrors = modelState
                    .SelectMany(modelStateEntry => modelStateEntry.Value.Errors)
                    .Select(modelError => modelError.ErrorMessage);

                var errors = new List<string>();
                foreach (var error in modelErrors)
                {
                    errors.Add(error);
                }

                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
