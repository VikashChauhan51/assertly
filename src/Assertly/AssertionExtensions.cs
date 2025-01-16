using Assertly.Primitives;

namespace Assertly;
public static class AssertionExtensions
{

    public static BooleanAssertions Should(this bool actualValue)
    {
        return new BooleanAssertions(actualValue, new AssertionChain());
    }
}
