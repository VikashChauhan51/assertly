using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Assertly;

public class AssertionChain
{
    private readonly List<string> failures = new List<string>();
    private string context = "object";
    private bool? succeeded;

    public AssertionChain ForCondition(bool condition)
    {
        if (!succeeded.HasValue || succeeded.Value)
        {
            succeeded = condition;
        }
        return this;
    }


    public AssertionChain FailWith(params object[] args)
    {
        if (succeeded.HasValue && !succeeded.Value)
        {
            var (caller, field) = DetermineCallerIdentity();
            var finalArgs = new object[args.Length + 1];
            finalArgs[0] = field;
            args.CopyTo(finalArgs, 1);
            string formattedMessage = string.Format(CultureInfo.InvariantCulture, "Expected {0} to be {1}, but found {2}.", finalArgs);
            failures.Add($"{caller}:{formattedMessage}");
            succeeded = null;
        }
        return this;
    }

    public bool HasFailures => failures.Count > 0;

    public string GetFailures()
    {
        return string.Join(Environment.NewLine, failures);
    }

    private (string caller, string field) DetermineCallerIdentity()
    {
        string? caller = null;
        string? field = null;
        try
        {
            var stack = new StackTrace(fNeedFileInfo: true);
            var allStackFrames = stack.GetFrames();

            int lastUserStackFrameBeforeFrameworkCodeIndex = Array.FindIndex(allStackFrames,
                frame => frame.GetMethod()?.DeclaringType?.Assembly != typeof(AssertionChain).Assembly);
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
                field = ExtractVariableNameFrom(frame);
            }
        }
        catch
        {
            // Ignore exceptions
        }
        return (caller ?? "Unknown Caller", field ?? context);
    }

    private static string? ExtractVariableNameFrom(StackFrame frame)
    {
        string? field = null;
        string statement = GetSourceCodeStatementFrom(frame);

        if (!string.IsNullOrEmpty(statement))
        {

            if (!IsBooleanLiteral(statement) && !IsNumeric(statement) && !IsStringLiteral(statement) &&
                !StartsWithNewKeyword(statement))
            {
                var match = Regex.Match(statement, @"(\w+)\.Should\(\)");
                if (match.Success)
                {
                    field = match.Groups[1].Value;
                }
            }
        }

        return field;
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
}



