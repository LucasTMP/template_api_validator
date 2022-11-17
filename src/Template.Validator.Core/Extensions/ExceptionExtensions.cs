using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Validator.Core.Extensions;

public static class ExceptionExtensions
{
    public static string ExceptionToText(this Exception exception)
        => $"{exception.Message} | {exception.Source} | {exception.StackTrace}";
}

