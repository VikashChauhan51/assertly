using Assertly.Core;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using VReflector;

namespace Assertly;
public abstract class AssertionsBase<T>
{
    protected readonly T? Subject;
    private bool? succeeded;
    private Func<string>? reason;
    protected AssertionsBase(T? subject)
    {
        Subject = subject;
    }

    internal AssertionsBase<T> ForCondition(bool condition)
    {
        if (!succeeded.HasValue || succeeded.Value)
        {
            succeeded = condition;
        }
        return this;
    }

    internal AssertionsBase<T> BecauseOf(string because, params object[] becauseArgs)
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
    internal void FailWith(string message, params object[] messageArgs)
    {
        if (succeeded.HasValue && !succeeded.Value)
        {
            var callerIdentifier = DetermineCallerIdentity();

            var formattedMessage = new FailureMessageFormatter()
                 .WithReason(reason?.Invoke() ?? string.Empty)
                 .WithIdentifier(callerIdentifier ?? "object")
                 .Format(message, messageArgs);
            Reset();
            throw new AssertlyException(formattedMessage);
        }
    }

    internal void FailWith(Action<string> onFailuer, string message, params object[] messageArgs)
    {
        if (succeeded.HasValue && !succeeded.Value)
        {
            var callerIdentifier = DetermineCallerIdentity();
            var formattedMessage = new FailureMessageFormatter()
                 .WithReason(reason?.Invoke() ?? string.Empty)
                 .WithIdentifier(callerIdentifier ?? "object")
                 .Format(message, messageArgs);
            Reset();
            onFailuer(formattedMessage);
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
    private static string? DetermineCallerIdentity()
    {
   
        string? callerIdentifier = null;
        try
        {
            var allStackFrames = IsStackTrace.GetStackFrames();
            int lastUserStackFrameBeforeFrameworkCodeIndex = Array.FindIndex(allStackFrames,
                frame => frame.GetMethod()?.DeclaringType?.Assembly != typeof(AssertionsBase<>).Assembly);
            if (lastUserStackFrameBeforeFrameworkCodeIndex >= 0)
            {
                var frame = allStackFrames[lastUserStackFrameBeforeFrameworkCodeIndex];
                callerIdentifier = ExtractVariableNameFrom(frame);
            }
        }
        catch
        {
            // Ignore exceptions
        }
        return callerIdentifier;
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
