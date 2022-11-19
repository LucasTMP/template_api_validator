using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Template.Validator.Core.Validator;

public abstract class BaseModelValidator<T> : AbstractValidator<T>, IValidatorInterceptor
{
    protected string RequestId { get; set; }

    public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
    {
        RequestId = actionContext.HttpContext.TraceIdentifier;
        return commonContext;
    }
    public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
    {
        actionContext.HttpContext.Items.Add(RequestId ?? "key", result);
        return result;
    }
}

