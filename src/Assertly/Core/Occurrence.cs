
namespace Assertly;
public abstract class Occurrence
{
    protected Occurrence(int expectedCount)
    {
        if (expectedCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(expectedCount), "Expected count cannot be negative.");
        }

        ExpectedCount = expectedCount;
    }

    internal int ExpectedCount { get; }
    internal abstract string Mode { get; }
    internal abstract bool Assert(int actual);
    internal void RegisterContextData(Action<string, object> register)
    {
        register("expectedOccurrence", $"{Mode} {Times(ExpectedCount)}");
    }
    public static string Times(int count) => count == 1 ? "1 time" : $"{count} times";
}
