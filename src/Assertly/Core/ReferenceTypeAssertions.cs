using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using VReflector;


namespace Assertly.Core;
public abstract class ReferenceTypeAssertions<TSubject, TAssertions>(TSubject? subject) : AssertionsBase<TSubject>(subject)
    where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
{

    public AndConstraint<TAssertions> BeNull([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be <null>{reason}, but found {0}.", EnsureSubject());

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
        ForCondition(Is.Same(Subject, expected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to refer to {0} {reason}, but found {1}.", EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeSameAs(TSubject unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(!Is.Same(Subject, unexpected))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {context} to refer to {0} {reason}.", EnsureType(unexpected));

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOfType(Type expectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(expectedType);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be {0} {reason}, but found <null>.", expectedType);

        Type subjectType = Subject!.GetType();

        if (expectedType.IsGenericTypeDefinition && IsType.GenericType(expectedType))
        {
            subjectType.GetGenericTypeDefinition().Assert().Be(expectedType, because, becauseArgs);
        }
        else
        {
            subjectType.Assert().Be(expectedType, because, becauseArgs);
        }


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeOfType(Type unexpectedType, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(unexpectedType);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} not to be {0} {reason}, but found <null>.", unexpectedType);

        Type subjectType = Subject!.GetType();

        if (unexpectedType.IsGenericTypeDefinition && IsType.GenericType(unexpectedType))
        {
            subjectType.GetGenericTypeDefinition().Assert().NotBe(unexpectedType, because, becauseArgs);
        }
        else
        {
            subjectType.Assert().NotBe(unexpectedType, because, becauseArgs);
        }


        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> BeAssignableTo(Type type, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(type);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be assignable to {0} {reason}, but found <null>.", type);

        ForCondition(Subject is not null && IsType.AssignableTo(Subject.GetType(), type))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be assignable to {0} {reason}, but {1} is not.", type, EnsureType(Subject?.GetType()));


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeAssignableTo(Type type,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(type);
            ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context} to not be assignable to {0} {reason}, but found <null>.", type);

        Type subjectType = Subject!.GetType();

        //TODO: need to change implmenttaion
            bool isAssignable = type.IsGenericTypeDefinition
                ? IsType.AssignableTo(subjectType, type)         //IsType.AssignableToOpenGeneric(subjectType,type)
                : IsType.AssignableTo(subjectType,type);
         
                ForCondition(!isAssignable)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected {context} to not be assignable to {0} {reason}, but {1} is.", type, Subject.GetType());
        

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> Match<T>(Expression<Func<T, bool>> predicate,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where T : TSubject
    {
        ArgumentNullException.ThrowIfNull(predicate);

        ForCondition(predicate.Compile()((T)Subject!))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context:object} to match {1}{reason}, but found {0}.", EnsureType(Subject), predicate);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}

