using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using VReflector;
namespace Assertly.Collections;
public class GenericCollectionAssertions<T>(IEnumerable<T> values) : GenericCollectionAssertions<IEnumerable<T>, T, GenericCollectionAssertions<T>>(values)
{
}

public class GenericCollectionAssertions<TCollection, T>(TCollection collection)
    : GenericCollectionAssertions<TCollection, T, GenericCollectionAssertions<TCollection, T>>(collection)
    where TCollection : IEnumerable<T>
{

}
public class GenericCollectionAssertions<TCollection, T, TAssertions>(TCollection collection) : ReferenceTypeAssertions<TCollection, TAssertions>(collection)
    where TCollection : IEnumerable<T>
    where TAssertions : GenericCollectionAssertions<TCollection, T, TAssertions>
{
    public AndConstraint<TAssertions> AllBeOfType(Type expectedType, [StringSyntax("CompositeFormat")] string because = "",
       params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(expectedType);

        ForCondition(Subject is not null)
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be {0} {reason}, but found {context:collection} is <null>.", expectedType.FullName);

        ForCondition(Subject!.All(x => x is not null))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be {0} {reason}, but found a null element.", expectedType.FullName);

        ForCondition(Subject!.All(x => expectedType == x.GetType()))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be {0} {reason}, but found some element not match.", expectedType.FullName);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeSequenceEqual(TCollection collection, [StringSyntax("CompositeFormat")] string because = "",
      params object[] becauseArgs)
    {

        ForCondition(Is.SequenceEqual(Subject!, collection))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be equal {reason}, but found all element not equal.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNotSequenceEqual(TCollection collection, [StringSyntax("CompositeFormat")] string because = "",
      params object[] becauseArgs)
    {

        ForCondition(!Is.SequenceEqual(Subject!, collection))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be not equal {reason}, but found all element equal.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

}
