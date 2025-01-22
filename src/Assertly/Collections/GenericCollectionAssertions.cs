using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
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
    public AndConstraint<TAssertions> BeEmpty([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();
        ForCondition(!Subject!.Any())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be empty {reason}, but found not empty.", EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeNullOrEmpty(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is null || !Subject.Any())
             .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be null or empty{reason}, but found not empty.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeNotEmpty([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject?.Any() ?? false)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to not be empty {reason}, but found empty.", EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
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
        Subject.Assert().NotBeNull();
        ForCondition(IsSequence.SequenceEqual(Subject!, collection))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be equal {reason}, but found all element not equal.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNotSequenceEqual(TCollection collection, [StringSyntax("CompositeFormat")] string because = "",
      params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();
        ForCondition(!IsSequence.SequenceEqual(Subject!, collection))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be not equal {reason}, but found all element equal.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeSequenceEqualIgnoreOrder(TCollection collection, [StringSyntax("CompositeFormat")] string because = "",
      params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();


        ForCondition(Subject.SequenceEqualIgnoreOrder(collection))
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be equal {reason}, but found all element not equal.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeInAscendingOrder([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        Subject.Assert().NotBeNull();

        ForCondition(Subject.IsSortedAscending())
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be in ascending order {reason}, but found all element not in ascending order.");

        return new AndConstraint<TAssertions>((TAssertions)this);

    }
    public AndConstraint<TAssertions> BeInDescendingOrder([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        Subject.Assert().NotBeNull();


        ForCondition(Subject.IsSortedDescending())
           .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:collection} to be in descending order {reason}, but found all element not in descending order.");

        return new AndConstraint<TAssertions>((TAssertions)this);

    }
    public AndConstraint<TAssertions> Contain(T expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject.Contains(expected))
        .FailWith("Expected {context:collection} {0} to contain {1}{reason}.", collection, expected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> Contain(Expression<Func<T, bool>> predicate,
        [StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        Subject.Assert().NotBeNull();
        Func<T, bool> func = predicate.Compile();
        bool condition = Subject.Any(func);
        BecauseOf(because, becauseArgs)
      .ForCondition(condition)
      .FailWith("Expected {context:collection} {0} to have an item matching {1}{reason}.", Subject, predicate.Body);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveCount(long expected, [StringSyntax("CompositeFormat")] string because = "",
       params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        var actualCount = Subject!.LongCount();

        ForCondition(actualCount == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:collection} to contain {0} item(s){reason}, but found {1}: {2}.", expected, actualCount, Subject);


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveCount(Expression<Func<long, bool>> countPredicate,
       [StringSyntax("CompositeFormat")] string because = "",
       params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(countPredicate);
        Subject.Assert().NotBeNull();

        Func<long, bool> compiledPredicate = countPredicate.Compile();

        var actualCount = Subject!.LongCount();

        ForCondition(!compiledPredicate(actualCount))
           .BecauseOf(because, becauseArgs)
           .FailWith("Expected {context:collection} to have a count {0}{reason}, but count is {1}: {2}.",
               countPredicate.Body, actualCount, Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);

    }
    public AndConstraint<TAssertions> HaveCountGreaterThan(long expected, [StringSyntax("CompositeFormat")] string because = "",
    params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        var actualCount = Subject!.LongCount();

        ForCondition(actualCount.GreaterThan(expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:collection} to contain more than {0} item(s){reason}, but found {1}.", expected, actualCount);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveCountGreaterThanOrEqual(long expected, [StringSyntax("CompositeFormat")] string because = "",
   params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        var actualCount = Subject!.LongCount();

        ForCondition(actualCount.GreaterThanOrEqual(expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:collection} to contain at least {0} item(s){reason}, but found {1}.", expected, actualCount);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> HaveCountLessThan(long expected, [StringSyntax("CompositeFormat")] string because = "",
  params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        var actualCount = Subject!.LongCount();

        ForCondition(actualCount.LessThan(expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:collection} to contain less than {0} item(s){reason}, but found {1}.", expected, actualCount);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> HaveCountLessThanOrEqual(long expected, [StringSyntax("CompositeFormat")] string because = "",
  params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();

        var actualCount = Subject!.LongCount();

        ForCondition(actualCount.LessThanOrEqual(expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:collection} to contain at most {0} item(s){reason}, but found {1}.", expected, actualCount);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> OnlyHaveUniqueItems(string because = "", params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();
        var (duplicate, count) = Subject.GetDuplicateItems();

        ForCondition(duplicate == 0)
                 .BecauseOf(because, becauseArgs)
                 .FailWith("Expected {context:collection} to only have unique items{reason}, but items {0} are not unique out of {1}.", duplicate, count);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> OnlyHaveUniqueItems(IEqualityComparer<T> comparer, string because = "", params object[] becauseArgs)
    {
        Subject.Assert().NotBeNull();
        var (duplicate, count) = Subject.GetDuplicateItems(comparer);

        ForCondition(duplicate == 0)
                 .BecauseOf(because, becauseArgs)
                 .FailWith("Expected {context:collection} to only have unique items{reason}, but items {0} are not unique out of {1}.", duplicate, count);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

}
