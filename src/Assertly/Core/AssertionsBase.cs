using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Assertly.Core;
public abstract class AssertionsBase<T>
{
    protected readonly T? Subject;
    private bool? succeeded;
    private Func<string>? reason;
    protected AssertionsBase(T? subject)
    {
        Subject = subject;
    }

    public AssertionsBase<T> ForCondition(bool condition)
    {
        if (!succeeded.HasValue || succeeded.Value)
        {
            succeeded = condition;
        }
        return this;
    }

    public AssertionsBase<T> BecauseOf(string because, params object[] becauseArgs)
    {
        reason = () =>
        {
            try
            {
                string becauseOrEmpty = because ?? string.Empty;

                return becauseArgs?.Length > 0
                    ? string.Format(CultureInfo.InvariantCulture, becauseOrEmpty, becauseArgs)
                    : becauseOrEmpty;
            }
            catch
            {
                return string.Empty;
            }
        };

        return this;
    }
    public void FailWith(string message, params object[] messageArgs)
    {
        if (succeeded.HasValue && !succeeded.Value)
        {
            var (caller, callerIdentifier) = DetermineCallerIdentity();

            var formattedMessage = new FailureMessageFormatter()
                 .WithReason(reason?.Invoke() ?? string.Empty)
                 .WithIdentifier(callerIdentifier ?? "object")
                 .Format(message, messageArgs);
            Reset();
            throw new AssertlyException($"{caller}:{formattedMessage}");
        }
    }

    public void FailWith(Action<string> onFailuer, string message, params object[] messageArgs)
    {
        if (succeeded.HasValue && !succeeded.Value)
        {
            var (caller, callerIdentifier) = DetermineCallerIdentity();
            var formattedMessage = new FailureMessageFormatter()
                 .WithReason(reason?.Invoke() ?? string.Empty)
                 .WithIdentifier(callerIdentifier ?? "object")
                 .Format(message, messageArgs);
            Reset();
            onFailuer($"{caller}:{formattedMessage}");
        }
    }

    protected object EnsureSubject() => AssertionHelper.EnsureType(Subject);
    protected object EnsureType(object? type) => AssertionHelper.EnsureType(type);

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        throw new NotSupportedException("Equals is not part of Assertly. Did you mean Be() instead?");
    }

    public override int GetHashCode()
    {
        return Subject?.GetHashCode() ?? 0;
    }
    private static (string caller, string? callerIdentifier) DetermineCallerIdentity()
    {
        string? caller = null;
        string? callerIdentifier = null;
        try
        {
            var stack = new StackTrace(fNeedFileInfo: true);
            var allStackFrames = stack.GetFrames();

            int lastUserStackFrameBeforeFrameworkCodeIndex = Array.FindIndex(allStackFrames,
                frame => frame.GetMethod()?.DeclaringType?.Assembly != typeof(AssertionsBase<>).Assembly);
            if (lastUserStackFrameBeforeFrameworkCodeIndex >= 0)
            {
                var frame = allStackFrames[lastUserStackFrameBeforeFrameworkCodeIndex];
                var method = frame.GetMethod();
                if (method != null)
                {
                    var property = method.Name;
                    var declaringType = method.DeclaringType?.FullName;
                    caller = $"{declaringType}.{property}";
                }
                callerIdentifier = ExtractVariableNameFrom(frame);
            }
        }
        catch
        {
            // Ignore exceptions
        }
        return (caller ?? string.Empty, callerIdentifier);
    }
    private static string? ExtractVariableNameFrom(StackFrame frame)
    {
        string? callerIdentifier = null;
        string? statement = GetSourceCodeStatementFrom(frame);

        if (!string.IsNullOrEmpty(statement) &&
            !IsBooleanLiteral(statement) &&
            !IsNumeric(statement) &&
            !IsStringLiteral(statement) &&
            !StartsWithNewKeyword(statement))
        {
            var match = Regex.Match(statement, @"(.+)\.Assert\(\)");
            if (match.Success)
            {
                callerIdentifier = match.Groups[1].Value;
            }
        }
        return callerIdentifier;
    }
    private static string? GetSourceCodeStatementFrom(StackFrame frame)
    {
        var fileName = frame.GetFileName();
        if (string.IsNullOrEmpty(fileName))
        {
            return null;
        }

        var lineNumber = frame.GetFileLineNumber();
        if (lineNumber == 0)
        {
            return null;
        }

        try
        {
            var lines = File.ReadAllLines(fileName);
            return lines[lineNumber - 1].Trim();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    private static bool IsBooleanLiteral(string statement) => statement == "true" || statement == "false";
    private static bool IsNumeric(string statement) => double.TryParse(statement, out _);
    private static bool IsStringLiteral(string statement) => statement.StartsWith("\"") && statement.EndsWith("\"");
    private static bool StartsWithNewKeyword(string statement) => statement.StartsWith("new ");
    private void Reset()
    {
        reason = null;
        succeeded = null;
    }
}
