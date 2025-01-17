using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assertly.Core;

internal static class AssertionHelper
{
    public static object EnsureType<T>(T value)
    {
        return value != null ? value : AssertionConstants.Null;
    }
}
