namespace Assertly;
public static class MoreThan
{
    public static Occurrence Once() => new MoreThanTimes(1);

    public static Occurrence Twice() => new MoreThanTimes(2);

    public static Occurrence Thrice() => new MoreThanTimes(3);

    public static Occurrence Times(int expected) => new MoreThanTimes(expected);

    private sealed class MoreThanTimes : Occurrence
    {
        internal MoreThanTimes(int expectedCount)
            : base(expectedCount)
        {
        }

        internal override string Mode => "more than";

        internal override bool Assert(int actual) => actual > ExpectedCount;
    }
}
