using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Validator.Core.Notifier;

namespace Template.Validator.Core.Interfaces.Notifier;
public interface INotification
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void AddNotification(Notification notification);
    void AddRangeNotification(IEnumerable<Notification> notification);
}
