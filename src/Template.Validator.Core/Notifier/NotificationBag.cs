using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Validator.Core.Interfaces.Notifier;

namespace Template.Validator.Core.Notifier;

public class NotificationBag : INotification
{
    private List<Notification> _notification { get; set; }
    public NotificationBag()
    {
        _notification = new();
    }
    public void AddNotification(Notification notification)
    {
        _notification.Add(notification);
    }

    public void AddRangeNotification(IEnumerable<Notification> notification)
    {
        _notification.AddRange(notification);
    }

    public List<Notification> GetNotifications()
    {
        return _notification;
    }

    public bool HasNotification()
    {
        return _notification.Any();
    }
}
