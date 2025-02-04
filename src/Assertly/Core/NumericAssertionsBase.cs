﻿using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Numerics;
using VReflector;

namespace Assertly;
public abstract class NumericAssertionsBase<T, TAssertions>(T? subject) : AssertionsBase<T?>(subject)
    where T : struct, INumber<T>
    where TAssertions : NumericAssertionsBase<T, TAssertions>
{
    public AndConstraint<TAssertions> Be(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is T subject && subject.CompareTo(expected) == 0)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:value} to be {0} {reason}, but found {1}" + GenerateDifferenceMessage(expected), expected,
            AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> Be(T? expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(expected is { } value ? Subject is T subject && subject.CompareTo(value) == 0 : Subject is not T)
       .BecauseOf(because, becauseArgs)
       .FailWith("Expected {context:value} to be {0} {reason}, but found {1}" + GenerateDifferenceMessage(expected), AssertionHelper.EnsureType(expected),
           AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> NotBe(T unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not T subject || subject.CompareTo(unexpected) != 0)
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context:value} to be {0} {reason}.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> NotBe(T? unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(unexpected is { } value ? Subject is not T subject || subject.CompareTo(value) != 0 : Subject is T)
            .BecauseOf(because, becauseArgs)
            .FailWith("Did not expect {context:value} to be {0} {reason}.", AssertionHelper.EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BePositive([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is T subject && subject.IsPositive())
             .BecauseOf(because, becauseArgs)
             .FailWith("Expected {context:value} to be positive{reason}, but found {0}.", AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeNegative([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is T value && !value.IsNaN() && value.IsNegative())
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:value} to be negative{reason}, but found {0}.", AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeLessThan(T expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (expected.IsNaN())
        {
            throw new ArgumentException("A value can never be less than NaN", nameof(expected));
        }

        ForCondition(Subject is T value && !value.IsNaN() && value.LessThan(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:value} to be less than {0} {reason}, but found {1}" + GenerateDifferenceMessage(expected),
                expected, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeLessThanOrEqualTo(T expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (expected.IsNaN())
        {
            throw new ArgumentException("A value can never be less than or equal to NaN", nameof(expected));
        }

        ForCondition(Subject is T value && !value.IsNaN() && value.LessThanOrEqual(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected {context:value} to be less than or equal to {0} {reason}, but found {1}" +
                GenerateDifferenceMessage(expected), expected, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeGreaterThan(T expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (expected.IsNaN())
        {
            throw new ArgumentException("A value can never be greater than NaN", nameof(expected));
        }

        ForCondition(Subject is T subject && subject.GreaterThan(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected {context:value} to be greater than {0} {reason}, but found {1}" + GenerateDifferenceMessage(expected),
                expected, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeGreaterThanOrEqualTo(T expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (IsNumerics.IsNaN(expected))
        {
            throw new ArgumentException("A value can never be greater than or equal to a NaN", nameof(expected));
        }

        ForCondition(Subject is T subject && subject.GreaterThanOrEqual(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected {context:value} to be greater than or equal to {0} {reason}, but found {1}" +
                GenerateDifferenceMessage(expected), expected, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeInRange(T minimumValue, T maximumValue,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (minimumValue.IsNaN() || maximumValue.IsNaN())
        {
            throw new ArgumentException("A range cannot begin or end with NaN");
        }

        ForCondition(Subject is T value && value.IsInRange(minimumValue, maximumValue))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:value} to be between {0} and {1} {reason}, but found {2}.",
                minimumValue, maximumValue, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> NotBeInRange(T minimumValue, T maximumValue,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        if (minimumValue.IsNaN() || maximumValue.IsNaN())
        {
            throw new ArgumentException("A range cannot begin or end with NaN");
        }

        ForCondition(Subject is T value && !value.IsInRange(minimumValue, maximumValue))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:value} to not be between {0} and {1} {reason}, but found {2}.",
                minimumValue, maximumValue, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeOneOf(params T[] validValues)
    {
        return BeOneOf(validValues, string.Empty);
    }
    public AndConstraint<TAssertions> BeOneOf(IEnumerable<T> validValues,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is T value && validValues.Contains(value))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:value} to be one of {0} {reason}, but found {1}.", validValues, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> BeOfType(Type expectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(expectedType);

        Type subjectType = Subject!.GetType();
        if (expectedType.IsGenericTypeDefinition && subjectType?.IsGenericType == true)
        {
            subjectType.GetGenericTypeDefinition().Assert().Be(expectedType, because, becauseArgs);
        }
        else
        {
            subjectType!.Assert().Be(expectedType, because, becauseArgs);
        }

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> NotBeOfType(Type unexpectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(unexpectedType);
        ForCondition(Subject is T)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type not to be " + unexpectedType + "{reason}, but found <null>.");

        Subject!.GetType().Assert().NotBe(unexpectedType, because, becauseArgs);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> Match(Expression<Func<T, bool>> predicate,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        ForCondition(Subject is T expression && predicate.Compile()(expression))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:value} to match {0} {reason}, but found {1}.", predicate.Body, AssertionHelper.EnsureType(Subject));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    private string GenerateDifferenceMessage(T? expected)
    {
        const string noDifferenceMessage = ".";

        if (Subject is not T subject || expected is not { } expectedValue)
        {
            return noDifferenceMessage;
        }

        var difference = CalculateDifference<T>(subject, expectedValue);
        return difference == default ? noDifferenceMessage : $" (difference of {difference}).";
    }
    private static T CalculateDifference<T>(T number1, T number2) where T : INumber<T>
    {
        return number1 - number2;
    }

}


