using FluentValidation.Results;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Validator.Core.Interfaces.Notifier;
using Template.Validator.Core.Models;
using Template.Validator.Core.Notifier;
using Microsoft.Extensions.Logging;
using Correlate;

namespace Template.Validator.Core.Services;

public abstract class BaseService
{
    protected readonly INotification _notification;
    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    protected BaseService(INotification notification, ICorrelationContextAccessor correlationContextAccessor)
    {
        _notification = notification;
        _correlationContextAccessor = correlationContextAccessor;
    }

    protected bool RunValidation<TV, TE>(TV validator, TE entity) where TV : AbstractValidator<TE> where TE : Entity
    {
        var result = validator.Validate(entity);
        if (result.IsValid) return true;
       var notifications = result.Errors.Select(e => new Notification(message : e.ErrorMessage, LogLevel.Error));
        _notification.AddRangeNotification(notifications);
        return false;
    }

    protected string GetCorrelationId() =>
        _correlationContextAccessor.CorrelationContext.CorrelationId;
    
    protected string SetCorrelationId(string correlationId) => 
        _correlationContextAccessor.CorrelationContext.CorrelationId = correlationId;
}
