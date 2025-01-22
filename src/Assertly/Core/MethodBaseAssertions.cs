using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using VReflector;


namespace Assertly.Core;
public abstract class MethodBaseAssertions<TSubject, TAssertions>(TSubject subject) : MemberInfoAssertions<TSubject, TAssertions>(subject)
    where TSubject : MethodBase
    where TAssertions : MethodBaseAssertions<TSubject, TAssertions>
{

    public AndConstraint<TAssertions> HaveAccessModifier(
        AccessModifier accessModifier,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected method to be {accessModifier}{{reason}}, but {{context:method}} is <null>.");

        AccessModifier subjectAccessModifier = Subject!.GetMethodAccessModifier();

        ForCondition(accessModifier == subjectAccessModifier)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Expected {Subject} to be {accessModifier}{{reason}}, but it is {subjectAccessModifier}.");
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    public AndConstraint<TAssertions> NotHaveAccessModifier(AccessModifier accessModifier,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected method not to be {accessModifier}{{reason}}, but {{context:member}} is <null>.");

        var subjectAccessModifier = Subject!.GetMethodAccessModifier();

        ForCondition(accessModifier != subjectAccessModifier)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Expected {Subject} not to be {accessModifier}{{reason}}, but it is.");


        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    internal static string GetParameterString(MethodBase methodBase)
    {
        IEnumerable<Type> parameterTypes = methodBase.GetParameters().Select(p => p.ParameterType);

        return string.Join(", ", parameterTypes.Select(p => p.FullName));
    }
}
