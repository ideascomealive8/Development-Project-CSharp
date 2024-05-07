using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sparcpoint.Exceptions;

namespace Interview.Web.Filters;

public class GeneralExceptionFilter: ExceptionFilterAttribute 
{
    public override void OnException(ExceptionContext context)
    {

        switch (context.Exception)
        {
            case ValidationException:
            {
                var result = new BadRequestObjectResult(context.Exception.Message);
            
                context.Result = result;
                break;
            }

            case ProductServiceException:
            {
                var result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
            
                context.Result = result;
                break;
            }

            default:
            {
                var result = new ObjectResult(context.Exception.Message)
                {
                    StatusCode = (int) HttpStatusCode.InternalServerError
                };
            
                context.Result = result;
                break;
            }
        }

        //EVAL: I usually do most of exception handling via filters
        //Its easy to wrap exceptions into error response
        //and code looks cleaner without dozens of try catch blocks
        //when more intelligent exception handling is required it should still absolutely be done 
    }
}