using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class DateTimeOffsetAssertions(DateTimeOffset? value) : DateTimeOffsetAssertions<DateTimeOffsetAssertions>(value)
{
}

public class DateTimeOffsetAssertions<TAssertions>(DateTimeOffset? value) : AssertionsBase<DateTimeOffset?>(value)
    where TAssertions : DateTimeOffsetAssertions<TAssertions>
{
    public AndConstraint<TAssertions> Be(DateTimeOffset expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Be(DateTimeOffset? expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetimeoffset} to be {0} {reason}, but found {1}.",
                EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(DateTimeOffset unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(DateTimeOffset? unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} not to be {0} {reason}, but it is.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeBefore(DateTimeOffset expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject < expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeBefore(DateTimeOffset unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return BeOnOrAfter(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOnOrBefore(DateTimeOffset expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject <= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be on or before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeOnOrBefore(DateTimeOffset unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return BeAfter(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeAfter(DateTimeOffset expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject > expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeAfter(DateTimeOffset unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return BeOnOrBefore(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOnOrAfter(DateTimeOffset expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject >= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be on or after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeOnOrAfter(DateTimeOffset unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return BeBefore(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> HaveYear(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .FailWith("Expected the year part of {context:datetimeoffset} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveYear(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .ForCondition(Subject.HasValue)
        .FailWith("Expected the year part of {context:datetimeoffset} not to be {0} {reason}, but found {1}.", unexpected,
            EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveMonth(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .FailWith("Expected the month part of {context:datetimeoffset} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveMonth(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .FailWith("Expected the month part of {context:datetimeoffset} not to be {0} {reason}, but found {1}.", unexpected,
            EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveDay(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .FailWith("Expected the day part of {context:datetimeoffset} to be {0} {reason}, but found {1}.", expected,
            EnsureType(Subject?.Day));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveDay(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
        .FailWith("Expected the day part of {context:datetimeoffset} not to be {0} {reason}, but found {1}.", unexpected,
            EnsureType(Subject?.Day));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(params DateTimeOffset?[] validValues)
    {
        return BeOneOf(validValues, string.Empty);
    }

    public AndConstraint<TAssertions> BeOneOf(params DateTimeOffset[] validValues)
    {
        return BeOneOf(validValues.Cast<DateTimeOffset?>());
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateTimeOffset> validValues, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return BeOneOf(validValues.Cast<DateTimeOffset?>(), because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateTimeOffset?> validValues, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:datetimeoffset} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

