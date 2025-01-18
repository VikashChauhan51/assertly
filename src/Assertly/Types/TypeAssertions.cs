using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using VReflector;

namespace Assertly.Types;
public class TypeAssertions(Type type) : ReferenceTypeAssertions<Type, TypeAssertions>(type)
{
    public AndConstraint<TypeAssertions> Be<TExpected>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return Be(typeof(TExpected), because, becauseArgs);
    }

    public AndConstraint<TypeAssertions> Be(Type expected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject == expected)
        .BecauseOf(because, becauseArgs)
        .FailWith(GetFailureMessageIfTypesAreDifferent(Subject, expected));

        return new AndConstraint<TypeAssertions>(this);
    }


    public new AndConstraint<TypeAssertions> BeAssignableTo(Type type,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(type);

        bool isAssignable = type.IsGenericTypeDefinition
            ? Subject is not null && Subject.IsAssignableTo(type)
            : type.IsAssignableFrom(Subject);

        BecauseOf(because, becauseArgs)
        .ForCondition(isAssignable)
        .FailWith("Expected {context:type} {0} to be assignable to {1} {reason}, but it is not.", Subject, type);

        return new AndConstraint<TypeAssertions>(this);
    }

    public new AndConstraint<TypeAssertions> NotBeAssignableTo(Type type,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(type);

        //TODO: use reflector
        bool isAssignable = type.IsGenericTypeDefinition
            ? Subject is not null && Subject.IsAssignableTo(type)
            : type.IsAssignableFrom(Subject);

        BecauseOf(because, becauseArgs)
        .ForCondition(!isAssignable)
        .FailWith("Expected {context:type} {0} to not be assignable to {1} {reason}, but it is.", Subject, type);

        return new AndConstraint<TypeAssertions>(this);
    }

    private static string GetFailureMessageIfTypesAreDifferent(Type actual, Type expected)
    {
        if (actual == expected)
        {
            return string.Empty;
        }

        string expectedType = expected?.FullName ?? AssertionConstants.Null;
        string actualType = actual?.FullName ?? AssertionConstants.Null;

        if (expectedType == actualType)
        {
            expectedType = "[" + expected.AssemblyQualifiedName + "]";
            actualType = "[" + actual.AssemblyQualifiedName + "]";
        }

        return $"Expected type to be {expectedType}{{reason}}, but found {actualType}.";
    }

    public AndConstraint<TypeAssertions> NotBe<TUnexpected>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return NotBe(typeof(TUnexpected), because, becauseArgs);
    }

    public AndConstraint<TypeAssertions> NotBe(Type unexpected,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        string nameOfUnexpectedType = unexpected is not null ? $"[{unexpected.AssemblyQualifiedName}]" : AssertionConstants.Null;

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject != unexpected)
        .FailWith("Expected type not to be " + nameOfUnexpectedType + "{reason}, but it is.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeDecoratedWith<TAttribute>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {

        ForCondition(Subject is not null && IsType.Attribute(Subject, typeof(TAttribute), false))
              .BecauseOf(because, becauseArgs)
            .FailWith("Expected type {0} to be decorated with {1} {reason}, but the attribute was not found.",
                Subject, typeof(TAttribute));

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeDecoratedWith<TAttribute>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
        where TAttribute : Attribute
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is null || !IsType.Attribute(Subject, typeof(TAttribute), false))
        .FailWith("Expected type {0} to not be decorated with {1} {reason}, but the attribute was found.",
            Subject, typeof(TAttribute));

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeSealed([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(IsType.Sealed(Subject!))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be sealed{reason}, but {context:type} is <null>.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeSealed([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(!IsType.Sealed(Subject!))
         .BecauseOf(because, becauseArgs)
         .FailWith("Expected type not to be sealed{reason}, but {context:type} is <null>.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeAbstract([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {


        ForCondition(IsType.Abstract(Subject!))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected type to be abstract{reason}, but {context:type} is <null>.");


        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeAbstract([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(!IsType.Abstract(Subject!))
            .BecauseOf(because, becauseArgs)
            .ForCondition(Subject is not null)
            .FailWith("Expected type not to be abstract{reason}, but {context:type} is <null>.");

        return new AndConstraint<TypeAssertions>(this);
    }
    public AndConstraint<TypeAssertions> BeStatic([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(IsType.Static(Subject!))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be static{reason}, but {context:type} is <null>.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeStatic([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(!IsType.Static(Subject!))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type not to be static{reason}, but {context:type} is <null>.");

        return new AndConstraint<TypeAssertions>(this);
    }

}
