using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class DateTimeAssertions : DateTimeAssertions<DateTimeAssertions>
{
    public DateTimeAssertions(DateTime? value) : base(value) { }
}

public class DateTimeAssertions<TAssertions> : AssertionsBase<DateTime?>
    where TAssertions : DateTimeAssertions<TAssertions>
{
    public DateTimeAssertions(DateTime? value) : base(value) { }

    public AndConstraint<TAssertions> Be(DateTime expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Be(DateTime? expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be {0} {reason}, but found {1}.", EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(DateTime unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(DateTime? unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} not to be {0} {reason}, but it is.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeBefore(DateTime expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject < expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeBefore(DateTime unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOnOrAfter(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOnOrBefore(DateTime expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject <= expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be on or before {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeOnOrBefore(DateTime unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeAfter(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeAfter(DateTime expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject > expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeAfter(DateTime unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOnOrBefore(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOnOrAfter(DateTime expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject >= expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be on or after {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeOnOrAfter(DateTime unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeBefore(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> HaveYear(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .FailWith("Expected the year part of {context:datetime} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveYear(int unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .ForCondition(Subject.HasValue)
            .FailWith("Expected the year part of {context:datetime} not to be {0} {reason}, but found {1}.", unexpected, EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveMonth(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .FailWith("Expected the month part of {context:datetime} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveMonth(int unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .FailWith("Expected the month part of {context:datetime} not to be {0} {reason}, but found {1}.", unexpected, EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveDay(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .FailWith("Expected the day part of {context:datetime} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Day));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveDay(int unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        BecauseOf(because, becauseArgs)
            .FailWith("Expected the day part of {context:datetime} not to be {0} {reason}, but found {1}.", unexpected, EnsureType(Subject?.Day));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(params DateTime?[] validValues)
    {
        return BeOneOf(validValues, string.Empty);
    }

    public AndConstraint<TAssertions> BeOneOf(params DateTime[] validValues)
    {
        return BeOneOf(validValues.Cast<DateTime?>());
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateTime> validValues, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOneOf(validValues.Cast<DateTime?>(), because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateTime?> validValues, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:datetime} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

