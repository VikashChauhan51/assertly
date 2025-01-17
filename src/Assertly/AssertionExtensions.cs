using Assertly.Core;
using Assertly.Primitives;

namespace Assertly;
public static class AssertionExtensions
{

    public static BooleanAssertions Assert(this bool? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new BooleanAssertions(null);
        }

        return new BooleanAssertions(actualValue.Value);
    }

    public static BooleanAssertions Assert(this bool actualValue)
    {

        return new BooleanAssertions(actualValue);
    }
}

