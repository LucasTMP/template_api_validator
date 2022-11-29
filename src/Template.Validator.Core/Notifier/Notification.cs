using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Validator.Core.Notifier;

public class Notification
{
    public Notification(string message, LogLevel level = LogLevel.Information, object data = null)
    {
        Message = message;
        Date = DateTime.Now;
        Level = level;
        Data = data;
    }

    public string Message { get; }
    public DateTime Date { get; }
    public LogLevel Level { get; }
    public object? Data { get; }
}
