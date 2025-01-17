using Assertly.Core;
using System.Diagnostics.CodeAnalysis;


namespace Assertly.Primitives;
public class DateOnlyAssertions(DateOnly? value) : DateOnlyAssertions<DateOnlyAssertions>(value)
{

}

public class DateOnlyAssertions<TAssertions>(DateOnly? value) : AssertionsBase<DateOnly?>(value)
    where TAssertions : DateOnlyAssertions<TAssertions>
{

    public AndConstraint<TAssertions> Be(DateOnly expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be {0} {reason}, but found {1}.",
            expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Be(DateOnly? expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:date} to be {0} {reason}, but found {1}.",
                EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBe(DateOnly unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {

        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBe(DateOnly? unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject != unexpected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} not to be {0} {reason}, but it is.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> BeBefore(DateOnly expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject < expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be before {0} {reason}, but found {1}.", expected,
            EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBeBefore(DateOnly unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOnOrAfter(unexpected, because, becauseArgs);
    }

    public AndConstraint<TAssertions> BeOnOrBefore(DateOnly expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject <= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be on or before {0} {reason}, but found {1}.", expected,
            EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBeOnOrBefore(DateOnly unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeAfter(unexpected, because, becauseArgs);
    }


    public AndConstraint<TAssertions> BeAfter(DateOnly expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject > expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be after {0} {reason}, but found {1}.", expected,
            EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBeAfter(DateOnly unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOnOrBefore(unexpected, because, becauseArgs);
    }


    public AndConstraint<TAssertions> BeOnOrAfter(DateOnly expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject >= expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be on or after {0} {reason}, but found {1}.", expected,
            EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotBeOnOrAfter(DateOnly unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeBefore(unexpected, because, becauseArgs);
    }


    public AndConstraint<TAssertions> HaveYear(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .FailWith("Expected the year part of {context:date} to be on or after {0} {reason}, but found {1}.", expected,
        EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotHaveYear(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
       
            BecauseOf(because, becauseArgs)
            .ForCondition(Subject.HasValue)
            .FailWith("Expected the year part of {context:date} to be on or after {0} {reason}, but found {1}.", unexpected,
        EnsureType(Subject?.Year));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> HaveMonth(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        
            BecauseOf(because, becauseArgs)
              .FailWith("Expected the month part of {context:date} to be on or after {0} {reason}, but found {1}.", expected,
        EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotHaveMonth(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        
            BecauseOf(because, becauseArgs)
            .FailWith("Expected the month part of {context:date} to be on or after {0} {reason}, but found {1}.", unexpected,
        EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> HaveDay(int expected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        
            BecauseOf(because, becauseArgs)
            .FailWith("Expected the day part of {context:date} to be on or after {0} {reason}, but found {1}.", expected,
        EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> NotHaveDay(int unexpected, [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .FailWith("Expected the day part of {context:date} to be on or after {0} {reason}, but found {1}.", unexpected,
        EnsureType(Subject?.Month));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> BeOneOf(params DateOnly?[] validValues)
    {
        return BeOneOf(validValues, string.Empty);
    }


    public AndConstraint<TAssertions> BeOneOf(params DateOnly[] validValues)
    {
        return BeOneOf(validValues.Cast<DateOnly?>());
    }


    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateOnly> validValues,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return BeOneOf(validValues.Cast<DateOnly?>(), because, becauseArgs);
    }


    public AndConstraint<TAssertions> BeOneOf(IEnumerable<DateOnly?> validValues,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:date} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


}
