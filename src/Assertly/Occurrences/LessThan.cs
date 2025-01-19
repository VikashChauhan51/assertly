namespace Assertly;
public static class LessThan
{
    public static Occurrence Twice() => new LessThanTimes(2);

    public static Occurrence Thrice() => new LessThanTimes(3);

    public static Occurrence Times(int expected) => new LessThanTimes(expected);

    private sealed class LessThanTimes : Occurrence
    {
        internal LessThanTimes(int expectedCount)
            : base(expectedCount)
        {
        }

        internal override string Mode => "less than";

        internal override bool Assert(int actual) => actual < ExpectedCount;
    }
}
