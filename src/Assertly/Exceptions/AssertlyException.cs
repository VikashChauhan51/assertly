namespace Assertly;
public class AssertlyException : Exception
{

    public AssertlyException() : base()
    {

    }
    public AssertlyException(string message) : base(message)
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        var className = GetType().ToString();
        var message = Message;
        string result;

        if (message == null || message.Length <= 0)
            result = className;
        else
            result = $"{className}: {message}";

        var stackTrace = StackTrace;
        if (stackTrace != null)
            result = $"{result}{Environment.NewLine}{stackTrace}";

        return result;
    }
}
