using System.Globalization;
using System.Text.RegularExpressions;

namespace Assertly.Core;
internal class FailureMessageFormatter
{
    private string? reason;
    private string? identifier;
    public FailureMessageFormatter WithReason(string reason)
    {
        this.reason = reason ?? string.Empty;
        return this;
    }

    public FailureMessageFormatter WithIdentifier(string identifier)
    {
        this.identifier = identifier;
        return this;
    }

    public string Format(string message, object[] messageArgs)
    {
        message = message.Replace("{reason}", reason, StringComparison.Ordinal);
        message = SubstituteIdentifier(message, identifier);
        try
        {
            return string.Format(CultureInfo.InvariantCulture, message, messageArgs);

        }
        catch
        {

            return string.Empty;
        }
        finally
        {
            Reset();
        }
    }

    private static string SubstituteIdentifier(string message, string? identifier)
    {
        const string pattern = @"(?:\s|^)\{context(?:\:(?<default>[a-z|A-Z|\s]+))?\}";

        message = Regex.Replace(message, pattern, match =>
        {
            const string result = " ";

            if (!string.IsNullOrEmpty(identifier))
            {
                return result + identifier;
            }

            string defaultIdentifier = match.Groups["default"].Value;

            if (!string.IsNullOrEmpty(defaultIdentifier))
            {
                return result + defaultIdentifier;
            }
            return " object";
        });

        return message.TrimStart();
    }

    public void Reset()
    {
        reason = null;
        identifier = null;
    }
}
