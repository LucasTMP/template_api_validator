using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using Template.Validator.Api.Controllers.Bases;

namespace Template.Validator.Api.Web_Flow.Filters;

    public class ContractValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                if (context.HttpContext.Items[context.HttpContext.TraceIdentifier] is ValidationResult result)
                {
                    var response = new DefaultResponse(
                        statusCode: HttpStatusCode.BadRequest,
                        success: false,
                        errors: result.Errors.Select(e => e.ErrorMessage));

                    context.Result = new BadRequestObjectResult(response);
                }else {  
                    var response = new DefaultResponse(
                        statusCode: HttpStatusCode.BadRequest,
                        success: false,
                        errors: context.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage));

                    context.Result = new BadRequestObjectResult(response);
                }
                return;
            }
            await next();
        }
    }
