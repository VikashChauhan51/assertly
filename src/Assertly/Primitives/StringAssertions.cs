using Assertly.Core;
using System.Diagnostics.CodeAnalysis;


namespace Assertly.Primitives;
public class StringAssertions(string? subject) : StringAssertions<StringAssertions>(subject)
{
}

public class StringAssertions<TAssertions>(string? subject) : ReferenceTypeAssertions<string, TAssertions>(subject)
    where TAssertions : StringAssertions<TAssertions>
{
    public AndConstraint<TAssertions> Be(string expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expected, Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(string unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to be {0}{reason}, but found {1}.", unexpected, Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> ContainSubstring(string expectedSubstring, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject.Contains(expectedSubstring))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to contain substring {0}{reason}, but it did not.", expectedSubstring);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> StartWith(string expectedPrefix, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject.StartsWith(expectedPrefix))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to start with {0}{reason}, but it did not.", expectedPrefix);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> EndWith(string expectedSuffix, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject.EndsWith(expectedSuffix))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to end with {0}{reason}, but it did not.", expectedSuffix);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveLength(int expectedLength, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && Subject.Length == expectedLength)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to have length {0}{reason}, but found length {1}.", expectedLength, Subject?.Length);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNullOrEmpty([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == null || Subject.Length == 0)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be null or empty{reason}, but it was not.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNullOrWhiteSpace([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == null || string.IsNullOrWhiteSpace(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be null or whitespace{reason}, but it was not.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> MatchRegex(string pattern, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && System.Text.RegularExpressions.Regex.IsMatch(Subject, pattern))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to match regex pattern {0}{reason}, but it did not.", pattern);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotMatchRegex(string pattern, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != null && !System.Text.RegularExpressions.Regex.IsMatch(Subject, pattern))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to match regex pattern {0}{reason}, but it did.", pattern);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
