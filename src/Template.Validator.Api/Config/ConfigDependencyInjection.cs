using Template.Validator.Api.WebFlow.Filters;
using Template.Validator.Core.Interfaces.Notifier;
using Template.Validator.Core.Notifier;

namespace Template.Validator.Api.Config;

public static class ConfigDependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotification, NotificationBag>();
    }
}

