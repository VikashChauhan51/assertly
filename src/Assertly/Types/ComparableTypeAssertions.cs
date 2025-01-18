using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using Reflector;

namespace Assertly.Types;

public class ComparableTypeAssertions<T>(IComparable<T> value) : ComparableTypeAssertions<T, ComparableTypeAssertions<T>>(value)
{
}


public class ComparableTypeAssertions<T, TAssertions>(IComparable<T> value) : ReferenceTypeAssertions<IComparable<T>, TAssertions>(value)
    where TAssertions : ComparableTypeAssertions<T, TAssertions>
{
    private const int Equal = 0;

    public AndConstraint<TAssertions> Be(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(IsEquals(Subject, expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} to be equal to {0}{reason}, but found {1}.", expected, Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(T unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(!IsEquals(Subject, unexpected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context:object} to be equal to {0}{reason}.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeRankedEquallyTo(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject?.CompareTo(expected) == Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} to be ranked as equal to {1}{reason}.", Subject, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeRankedEquallyTo(T unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject?.CompareTo(unexpected) != Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} not to be ranked as equal to {1}{reason}.", Subject, unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeLessThan(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject?.CompareTo(expected) < Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} to be less than {1}{reason}.", Subject, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeLessThanOrEqualTo(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject?.CompareTo(expected) <= Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} to be less than or equal to {1}{reason}.", Subject, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> BeGreaterThan(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject?.CompareTo(expected) > Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} to be greater than {1}{reason}.", Subject, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeGreaterThanOrEqualTo(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject?.CompareTo(expected) >= Equal)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} {0} to be greater than or equal to {1}{reason}.", Subject, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    private static bool IsEquals(object? first, object? second)
    {
        return Equals(first, second);
    }
}
