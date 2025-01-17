using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class TimeSpanAssertions : TimeSpanAssertions<TimeSpanAssertions>
{
    public TimeSpanAssertions(TimeSpan? value) : base(value) { }
}

public class TimeSpanAssertions<TAssertions> : AssertionsBase<TimeSpan?>
    where TAssertions : TimeSpanAssertions<TAssertions>
{
    public TimeSpanAssertions(TimeSpan? value) : base(value) { }

    public AndConstraint<TAssertions> Be(TimeSpan expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(TimeSpan unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeGreaterThan(TimeSpan expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject > expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be greater than {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeGreaterThanOrEqualTo(TimeSpan expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject >= expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be greater than or equal to {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeLessThan(TimeSpan expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject < expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be less than {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeLessThanOrEqualTo(TimeSpan expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject <= expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be less than or equal to {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveHours(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject?.Hours == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected the hours part of {context:timespan} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Hours));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveMinutes(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject?.Minutes == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected the minutes part of {context:timespan} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Minutes));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveSeconds(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject?.Seconds == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected the seconds part of {context:timespan} to be {0} {reason}, but found {1}.", expected, EnsureType(Subject?.Seconds));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<TimeSpan?> validValues, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:timespan} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(params TimeSpan[] validValues)
    {
        return BeOneOf(validValues.Cast<TimeSpan?>());
    }
}

