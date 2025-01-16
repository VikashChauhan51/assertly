namespace Assertly;
public class TrueException : AssertlyException
{
    TrueException(string message) : base(message)
    {
    }

    public static TrueException ForNonTrueValue(bool value, string? message = null)
    {
        return new TrueException(
                    message != null
                        ? message
                        : "Assertly.True() Failure" + Environment.NewLine +
                          "Expected: True" + Environment.NewLine +
                          "Actual:   " + (value)
                );
    }
}
