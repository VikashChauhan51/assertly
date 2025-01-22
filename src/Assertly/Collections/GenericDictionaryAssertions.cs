using System.Diagnostics.CodeAnalysis;

namespace Assertly.Collections;
public class GenericDictionaryAssertions<TCollection, TKey, TValue>(TCollection keyValuePairs)
    : GenericDictionaryAssertions<TCollection, TKey, TValue, GenericDictionaryAssertions<TCollection, TKey, TValue>>(keyValuePairs)
    where TCollection : IEnumerable<KeyValuePair<TKey, TValue>>
{

}

public class GenericDictionaryAssertions<TCollection, TKey, TValue, TAssertions>(TCollection keyValuePairs)
    : GenericCollectionAssertions<TCollection, KeyValuePair<TKey, TValue>, TAssertions>(keyValuePairs)
    where TCollection : IEnumerable<KeyValuePair<TKey, TValue>>
    where TAssertions : GenericDictionaryAssertions<TCollection, TKey, TValue, TAssertions>
{

    public AndConstraint<TAssertions> Equal<T>(T expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where T : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        ArgumentNullException.ThrowIfNull(nameof(expected));

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but found {1}.", expected, Subject);

        IEnumerable<TKey> subjectKeys = GetKeys(Subject);
        IEnumerable<TKey> expectedKeys = GetKeys(expected);
        IEnumerable<TKey> missingKeys = expectedKeys.Except(subjectKeys);
        IEnumerable<TKey> additionalKeys = subjectKeys.Except(expectedKeys);

        ForCondition(missingKeys.Any())
            .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but could not find keys {1}.", expected,
                    missingKeys);

        ForCondition(additionalKeys.Any())
            .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but found additional keys {1}.",
                    expected,
                    additionalKeys);

        var comparer = Comparer<TValue>.Default;

        foreach (var key in expectedKeys)
        {

            ForCondition(comparer.Compare(GetValue(Subject, key), GetValue(expected, key)) == 0)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:dictionary} to be equal to {0}{reason}, but {1} differs at key {2}.",
                expected, Subject, key);
        }


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotEqual<T>(T unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where T : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        ArgumentNullException.ThrowIfNull(nameof(unexpected));

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected dictionaries not to be equal{reason}, but found {0}.", Subject);

        ForCondition(!ReferenceEquals(Subject, unexpected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected dictionaries not to be equal{reason}, but they both reference the same object.");

        IEnumerable<TKey> subjectKeys = GetKeys(Subject);
        IEnumerable<TKey> unexpectedKeys = GetKeys(unexpected);
        IEnumerable<TKey> missingKeys = unexpectedKeys.Except(subjectKeys);
        IEnumerable<TKey> additionalKeys = subjectKeys.Except(unexpectedKeys);

        var comparer = Comparer<TValue>.Default;

        bool foundDifference = missingKeys.Any()
            || additionalKeys.Any()
            || subjectKeys.Any(key => comparer.Compare(GetValue(Subject, key), GetValue(unexpected, key)) != 0);

        ForCondition(!foundDifference)
            .BecauseOf(because, becauseArgs)
            .FailWith("Did not expect dictionaries {0} and {1} to be equal{reason}.", unexpected, Subject);


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    private TValue? GetValue(IEnumerable<KeyValuePair<TKey, TValue>> subject, TKey key)
    {
        if (subject is not null)
        {
            foreach (var kvp in from KeyValuePair<TKey, TValue> kvp in subject
                                where EqualityComparer<TKey>.Default.Equals(kvp.Key, key)
                                select kvp)
            {
                return kvp.Value;
            }
        }
        return default(TValue);
    }

    private IEnumerable<TKey> GetKeys(IEnumerable<KeyValuePair<TKey, TValue>> subject)
    {
        foreach (var kvp in subject)
        {
            yield return kvp.Key;
        }
    }
}
