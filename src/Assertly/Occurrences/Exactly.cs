namespace Assertly;
public static class Exactly
{
    public static Occurrence Once() => new ExactlyTimes(1);

    public static Occurrence Twice() => new ExactlyTimes(2);

    public static Occurrence Thrice() => new ExactlyTimes(3);

    public static Occurrence Times(int expected) => new ExactlyTimes(expected);

    private sealed class ExactlyTimes : Occurrence
    {
        internal ExactlyTimes(int expectedCount)
            : base(expectedCount)
        {
        }

        internal override string Mode => "exactly";

        internal override bool Assert(int actual) => actual == ExpectedCount;
    }
}
