using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Assertly.Core;
public abstract class MemberInfoAssertions<TSubject, TAssertions>(TSubject subject) : ReferenceTypeAssertions<TSubject, TAssertions>(subject)
    where TSubject : MemberInfo
    where TAssertions : MemberInfoAssertions<TSubject, TAssertions>
{


    public AndWhichConstraint<MemberInfoAssertions<TSubject, TAssertions>, TAttribute> BeDecoratedWith<TAttribute>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {
        return BeDecoratedWith<TAttribute>(_ => true, because, becauseArgs);
    }

    public AndConstraint<TAssertions> NotBeDecoratedWith<TAttribute>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {
        return NotBeDecoratedWith<TAttribute>(_ => true, because, becauseArgs);
    }

    public AndWhichConstraint<MemberInfoAssertions<TSubject, TAssertions>, TAttribute> BeDecoratedWith<TAttribute>(
        Expression<Func<TAttribute, bool>> isMatchingAttributePredicate,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {

        ArgumentNullException.ThrowIfNull(isMatchingAttributePredicate);

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith(
                $"Expected {Identifier} to be decorated with {typeof(TAttribute)}{{reason}}" +
                ", but {context:member} is <null>.");

       var attributes = GetMatchingAttributes(Subject, isMatchingAttributePredicate);
        ForCondition(attributes.Any())
        .BecauseOf(because, becauseArgs)
        .FailWith(
            $"Expected {Identifier} {SubjectDescription} to be decorated with {typeof(TAttribute)}{{reason}}" +
            ", but that attribute was not found.");

        return new AndWhichConstraint<MemberInfoAssertions<TSubject, TAssertions>, TAttribute>(this, attributes.First());
    }

    public AndConstraint<TAssertions> NotBeDecoratedWith<TAttribute>(
        Expression<Func<TAttribute, bool>> isMatchingAttributePredicate,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
        where TAttribute : Attribute
    {
        ArgumentNullException.ThrowIfNull(isMatchingAttributePredicate);

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith(
                $"Expected {Identifier} to not be decorated with {typeof(TAttribute)}{{reason}}" +
                ", but {context:member} is <null>.");

        IEnumerable<TAttribute> attributes = GetMatchingAttributes(Subject!, isMatchingAttributePredicate);

        ForCondition(!attributes.Any())
        .BecauseOf(because, becauseArgs)
        .FailWith(
            $"Expected {Identifier} {SubjectDescription} to not be decorated with {typeof(TAttribute)}{{reason}}" +
            ", but that attribute was found.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    private static IEnumerable<TAttribute> GetMatchingAttributes<TAttribute>(MemberInfo memberInfo,
       Expression<Func<TAttribute, bool>> isMatchingAttributePredicate)
       where TAttribute : Attribute
    {
        var customAttributes = memberInfo.GetCustomAttributes<TAttribute>(inherit: false).ToList();

        if (typeof(TAttribute) == typeof(MethodImplAttribute) && memberInfo is MethodBase methodBase)
        {
            (bool success, MethodImplAttribute? methodImplAttribute) = RecreateMethodImplAttribute(methodBase);
            if (success)
            {
                var attribute = methodImplAttribute as TAttribute;
                if (attribute != null)
                {
                    customAttributes.Add(attribute);
                }
            }
        }

        return customAttributes
            .Where(isMatchingAttributePredicate.Compile());
    }

    private static bool IsNonVirtual(MethodInfo method)
    {
        return !method.IsVirtual || method.IsFinal;
    }
    private static (bool success, MethodImplAttribute? attribute) RecreateMethodImplAttribute(MethodBase methodBase)
    {
        MethodImplAttributes implementationFlags = methodBase.MethodImplementationFlags;

        int implementationFlagsMatchingImplementationOptions =
            (int)implementationFlags & Enum.GetValues(typeof(MethodImplOptions)).Cast<int>().Sum(x => x);

        MethodImplOptions implementationOptions = (MethodImplOptions)implementationFlagsMatchingImplementationOptions;

        if (implementationOptions != default)
        {
            return (true, new MethodImplAttribute(implementationOptions));
        }

        return (false, null);
    }
    protected string Identifier => "member";
    private protected virtual string SubjectDescription => $"{Subject?.DeclaringType}.{Subject?.Name}";
}
