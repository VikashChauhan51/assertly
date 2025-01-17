using Assertly.Primitives;

namespace Assertly;
public static class AssertionExtensions
{

    public static BooleanAssertions Should(this bool? actualValue)
    {
        if (!actualValue.HasValue)
        {
            return new BooleanAssertions(null, new AssertionChain());
        }

        return new BooleanAssertions(actualValue.Value, new AssertionChain());
    }

    public static BooleanAssertions Should(this bool actualValue)
    {

        return new BooleanAssertions(actualValue, new AssertionChain());
    }
}

