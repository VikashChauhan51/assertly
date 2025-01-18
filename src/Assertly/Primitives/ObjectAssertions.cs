using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Assertly.Primitives;

public class ObjectAssertions(object? value): ObjectAssertions<object, ObjectAssertions>(value)
{
 
}
    public class ObjectAssertions<TSubject, TAssertions>(TSubject? subject) : ReferenceTypeAssertions<TSubject, TAssertions>(subject)
    where TAssertions : ObjectAssertions<TSubject, TAssertions>
{
    public AndConstraint<TAssertions> BeEquivalentTo(TSubject expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(expected is not null && Subject is not null && expected.Equals(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be equivalent to {0}{reason}, but found {1}.", EnsureType(expected), EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeEquivalentTo(TSubject unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && !Subject.Equals(unexpected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to be equivalent to {0}{reason}.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveSameHashCode(TSubject other, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && other is not null && Subject.GetHashCode() == other.GetHashCode())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to have the same hash code as {0}{reason}, but found {1}.", EnsureType(other), EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotHaveSameHashCode(TSubject other, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && other is not null && Subject.GetHashCode() != other.GetHashCode())
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to have the same hash code as {0}{reason}, but found {1}.", EnsureType(other), EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeDefault([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        var defaultValue = default(TSubject);
        ForCondition(Equals(Subject, defaultValue))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to have the default value {0}{reason}, but found {1}.", defaultValue, EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeDefault([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        var defaultValue = default(TSubject);
        ForCondition(!Equals(Subject, defaultValue))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to have the default value {0}{reason}, but found {1}.", defaultValue, EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Satisfy(Expression<Func<TSubject, bool>> condition, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(condition);

        ForCondition(Subject != null && condition.Compile()(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} to satisfy condition{reason}, but it did not.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
