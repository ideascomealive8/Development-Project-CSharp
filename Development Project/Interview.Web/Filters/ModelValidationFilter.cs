using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Interview.Web.Filters;

//this would validate any model passed into actions
public class ModelValidationFilter: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {        
        if (context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
