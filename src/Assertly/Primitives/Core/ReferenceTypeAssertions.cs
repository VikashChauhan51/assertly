﻿using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;


namespace Assertly.Primitives.Core;
public abstract class ReferenceTypeAssertions<TSubject, TAssertions>(TSubject subject) : AssertionsBase<TSubject>(subject)
    where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
{

    public AndConstraint<TAssertions> BeNull([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be <null>{reason}, but found {0}.", Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeNull([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} not to be <null>{reason}.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeSameAs(TSubject expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(ReferenceEquals(Subject, expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to refer to {0}{reason}, but found {1}.", expected, Subject);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    /// <summary>
    /// Asserts that the object is not the same reference as another.
    /// </summary>
    public AndConstraint<TAssertions> NotBeSameAs(TSubject unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(!ReferenceEquals(Subject, unexpected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to refer to {0}{reason}.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOfType(Type expectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(expectedType);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be {0}{reason}, but found <null>.", expectedType);

        //TODO:pending
        //Type subjectType = Subject.GetType();
        //if (expectedType.IsGenericTypeDefinition && subjectType.IsGenericType)
        //{
        //    subjectType.GetGenericTypeDefinition().Should().Be(expectedType, because, becauseArgs);
        //}
        //else
        //{
        //    subjectType.Should().Be(expectedType, because, becauseArgs);
        //}


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    /// <summary>
    /// Asserts that the object is not of the specified type.
    /// </summary>
    public AndConstraint<TAssertions> NotBeOfType(Type unexpectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(unexpectedType);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} not to be {0}{reason}, but found <null>.", unexpectedType);

        //TODO:pending

        //Type subjectType = Subject.GetType();
        //if (unexpectedType.IsGenericTypeDefinition && subjectType.IsGenericType)
        //{
        //    subjectType.GetGenericTypeDefinition().Should().NotBe(unexpectedType, because, becauseArgs);
        //}
        //else
        //{
        //    subjectType.Should().NotBe(unexpectedType, because, becauseArgs);
        //}


        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> BeAssignableTo(Type type, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(type);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be assignable to {0}{reason}, but found <null>.", type);

        ForCondition(Subject is not null && type.IsAssignableFrom(Subject.GetType()))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be assignable to {0}{reason}, but {1} is not.", type, Subject.GetType());


        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

