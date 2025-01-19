using Assertly.Core;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security.AccessControl;
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

        ForCondition(Is.Same(Subject, expected))
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

        bool isAssignable = type.IsGenericTypeDefinition
            ? Subject is not null && Subject.IsAssignableTo(type)
            : type.IsAssignableFrom(Subject);

        BecauseOf(because, becauseArgs)
        .ForCondition(!isAssignable)
        .FailWith("Expected {context:type} {0} to not be assignable to {1} {reason}, but it is.", Subject, type);

        return new AndConstraint<TypeAssertions>(this);
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
        .ForCondition(!Is.Same(Subject, unexpected))
        .FailWith("Expected type not to be " + nameOfUnexpectedType + "{reason}, but it is.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeDecoratedWithOrInherit<TAttribute>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {

        ForCondition(Subject is not null && IsType.Attribute(Subject, typeof(TAttribute), true))
              .BecauseOf(because, becauseArgs)
            .FailWith("Expected type {0} to be decorated with {1} {reason}, but the attribute was not found.",
                Subject, typeof(TAttribute));

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

    public AndConstraint<TypeAssertions> NotBeDecoratedWithOrInherit<TAttribute>([StringSyntax("CompositeFormat")] string because = "",
       params object[] becauseArgs)
       where TAttribute : Attribute
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is null || !IsType.Attribute(Subject, typeof(TAttribute), true))
        .FailWith("Expected type {0} to not be decorated with {1} {reason}, but the attribute was found.",
            Subject, typeof(TAttribute));

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeClass([StringSyntax("CompositeFormat")] string because = "",
       params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be class {reason}, but {context:type} is <null>.");

        ForCondition(IsClass())
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected type to be class {reason}, but {context:type} is not.");

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeClass([StringSyntax("CompositeFormat")] string because = "",
      params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type to be class {reason}, but {context:type} is <null>.");

        ForCondition(!IsClass())
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected type not to be class {reason}, but {context:type} is a class.");

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

    public AndConstraint<TypeAssertions> Implement<TInterface>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
        where TInterface : class
    {
        return Implement(typeof(TInterface), because, becauseArgs);
    }
    public AndConstraint<TypeAssertions> Implement(Type interfaceType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(interfaceType);

        ForCondition(IsType.Interface(interfaceType))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {0} to be an interface, but it is not.", interfaceType);

        ForCondition(IsType.Interface(interfaceType) && interfaceType != Subject && interfaceType.IsAssignableFrom(Subject))
         .BecauseOf(because, becauseArgs)
         .FailWith("Expected {0} to implement interface {1} {reason}, but {0} does not implement it.", Subject, interfaceType);

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotImplement<TInterface>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
        where TInterface : class
    {
        return NotImplement(typeof(TInterface), because, becauseArgs);
    }
    public AndConstraint<TypeAssertions> NotImplement(Type interfaceType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(interfaceType);

        ForCondition(Subject is not null)
           .BecauseOf(because, becauseArgs)
           .FailWith("Expected {0} to be an interface, but it is null.", interfaceType);
        ForCondition(IsType.Interface(interfaceType))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {0} to be an interface, but it is not.", interfaceType);

        ForCondition(IsType.Interface(interfaceType) && interfaceType != Subject && !interfaceType.IsAssignableFrom(Subject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {0} to not implement interface {1} {reason}, but {0} does implement it.", Subject, interfaceType);

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> BeDerivedFrom<TBaseClass>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
        where TBaseClass : class
    {
        return BeDerivedFrom(typeof(TBaseClass), because, becauseArgs);
    }
    public AndConstraint<TypeAssertions> BeDerivedFrom(Type baseType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        ForCondition(!IsType.Interface(baseType))
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected {0} to be a class, but it is an interface.", baseType);

        bool isDerivedFrom = baseType.IsGenericTypeDefinition
            ? IsType.DerivedFromOpenGeneric(Subject, baseType)
            : Subject != null && Subject.IsSubclassOf(baseType);
        ForCondition(isDerivedFrom)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected type {0} to be derived from {1} {reason}, but it is not.", Subject, baseType);

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndConstraint<TypeAssertions> NotBeDerivedFrom<TBaseClass>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
        where TBaseClass : class
    {
        return NotBeDerivedFrom(typeof(TBaseClass), because, becauseArgs);
    }
    public AndConstraint<TypeAssertions> NotBeDerivedFrom(Type baseType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(baseType);

        ForCondition(Subject is not null)
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected {0} to be an class, but it is null.", baseType);

        ForCondition(!IsType.Interface(baseType))
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected {0} to be a class, but it is an interface.", baseType);

        bool isDerivedFrom = baseType.IsGenericTypeDefinition
            ? IsType.DerivedFromOpenGeneric(Subject, baseType)
            : Subject != null && Subject.IsSubclassOf(baseType);
        ForCondition(!isDerivedFrom)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type {0} to be not derived from {1} {reason}, but it is.", Subject, baseType);

        return new AndConstraint<TypeAssertions>(this);
    }

    public AndWhichConstraint<TypeAssertions, PropertyInfo> HaveProperty<TProperty>(
       string name, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return HaveProperty(typeof(TProperty), name, because, becauseArgs);
    }
    public AndWhichConstraint<TypeAssertions, PropertyInfo> HaveProperty(
        Type propertyType, string name,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(propertyType);
        ArgumentNullException.ThrowIfNull(name);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Cannot determine if a type has a property named {name} if the type is <null>.");

        PropertyInfo? propertyInfo = Subject!.GetProperty(name);

        ForCondition(propertyInfo is not null)
       .BecauseOf(because, becauseArgs)
        .FailWith($"Expected {Subject.Name} to have a property {name} of type {propertyType.Name} {{reason}}, but it does not.");

        return new AndWhichConstraint<TypeAssertions, PropertyInfo>(this, propertyInfo!);
    }

    public AndConstraint<TypeAssertions> NotHaveProperty<TProperty>(
       string name, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return NotHaveProperty(typeof(TProperty), name, because, becauseArgs);
    }
    public AndConstraint<TypeAssertions> NotHaveProperty(
        Type propertyType, string name,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(propertyType);
        ArgumentNullException.ThrowIfNull(name);

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Cannot determine if a type has a property named {name} if the type is <null>.");

        PropertyInfo? propertyInfo = Subject!.GetProperty(name);

        ForCondition(propertyInfo is null)
       .BecauseOf(because, becauseArgs)
        .FailWith($"Expected {Subject.Name} to not have a property {name} of type {propertyType.Name} {{reason}}, but it have not.");

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
    private bool IsClass()
    {
        if (Subject is null ||
            Subject.IsInterface ||
            Subject.IsValueType ||
            typeof(Delegate).IsAssignableFrom(Subject.BaseType) ||
            IsType.Primitive(Subject))
        {
            return false;
        }

        return IsType.Class(Subject) || IsType.RecordClass(Subject);
    }
}
