using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class TimeOnlyAssertions(TimeOnly? value) : TimeOnlyAssertions<TimeOnlyAssertions>(value)
{
}

public class TimeOnlyAssertions<TAssertions>(TimeOnly? value) : AssertionsBase<TimeOnly?>(value)
    where TAssertions : TimeOnlyAssertions<TAssertions>
{
    public AndConstraint<TAssertions> Be(TimeOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be {0} {reason}, but found {1}.",
            expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Be(TimeOnly? expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be {0} {reason}, but found {1}.",
            EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(TimeOnly unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(TimeOnly? unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} not to be {0} {reason}, but it is.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeBefore(TimeOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject < expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOnOrBefore(TimeOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject <= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be on or before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeAfter(TimeOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject > expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOnOrAfter(TimeOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject >= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be on or after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(params TimeOnly?[] validValues)
    {
        return BeOneOf(validValues, string.Empty);
    }

    public AndConstraint<TAssertions> BeOneOf(params TimeOnly[] validValues)
    {
        return BeOneOf(validValues.Cast<TimeOnly?>());
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<TimeOnly> validValues,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOneOf(validValues.Cast<TimeOnly?>(), because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<TimeOnly?> validValues,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:time} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveHour(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject?.Hour == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the hour part of {context:time} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Hour));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveMinute(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject?.Minute == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the minute part of {context:time} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Minute));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveSecond(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject?.Second == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the second part of {context:time} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Second));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

