using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using VReflector;

namespace Assertly.Types;
public class MethodInfoAssertions(MethodInfo methodInfo) : MethodBaseAssertions<MethodInfo, MethodInfoAssertions>(methodInfo)
{

    public AndConstraint<MethodInfoAssertions> BeVirtual([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected method to be virtual{reason}, but {context:member} is <null>.");

        ForCondition(!Subject.IsNonVirtual())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected method " + SubjectDescription + " to be virtual{reason}, but it is not virtual.");


        return new AndConstraint<MethodInfoAssertions>(this);
    }

    public AndConstraint<MethodInfoAssertions> NotBeVirtual([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
             .BecauseOf(because, becauseArgs)
             .FailWith("Expected method to be virtual{reason}, but {context:member} is <null>.");

        ForCondition(Subject.IsNonVirtual())
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected method " + SubjectDescription + " not to be virtual{reason}, but it is.");


        return new AndConstraint<MethodInfoAssertions>(this);
    }

    public AndConstraint<MethodInfoAssertions> BeAsync([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith("Expected method to be async{reason}, but {context:member} is <null>.");

        ForCondition(Subject.IsAsync())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected method " + SubjectDescription + " to be async{reason}, but it is not.");


        return new AndConstraint<MethodInfoAssertions>(this);
    }
    public AndConstraint<MethodInfoAssertions> NotBeAsync([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith("Expected method not to be async{reason}, but {context:member} is <null>.");

        ForCondition(!Subject.IsAsync())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected method " + SubjectDescription + " not to be async{reason}, but it is.");


        return new AndConstraint<MethodInfoAssertions>(this);
    }

    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> ReturnVoid(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith("Expected the return type of method to be void{reason}, but {context:member} is <null>.");

        ForCondition(typeof(void) == Subject!.ReturnType)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the return type of method " + Subject.Name + " to be void{reason}, but it is {0}.",
            Subject.ReturnType.FullName);


        return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
    }

    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> Return(Type returnType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(returnType);

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith("Expected the return type of method to be {0}{reason}, but {context:member} is <null>.", returnType);

        ForCondition(returnType == Subject!.ReturnType)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the return type of method " + Subject.Name + " to be {0}{reason}, but it is {1}.",
            returnType, Subject.ReturnType.FullName);


        return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
    }


    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> Return<TReturn>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return Return(typeof(TReturn), because, becauseArgs);
    }


    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> NotReturnVoid(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith("Expected the return type of method not to be void{reason}, but {context:member} is <null>.");

        ForCondition(typeof(void) != Subject!.ReturnType)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected the return type of method " + Subject.Name + " not to be void{reason}, but it is.");


        return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
    }


    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> NotReturn(Type returnType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(returnType);


        BecauseOf(because, becauseArgs)
        .ForCondition(Subject is not null)
        .FailWith(
            "Expected the return type of method not to be {0}{reason}, but {context:member} is <null>.", returnType);

        ForCondition(returnType != Subject!.ReturnType)
        .BecauseOf(because, becauseArgs)
        .FailWith(
            "Expected the return type of method " + Subject.Name + " not to be {0}{reason}, but it is.", returnType);

        return new AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>>(this);
    }

    public AndConstraint<MethodBaseAssertions<MethodInfo, MethodInfoAssertions>> NotReturn<TReturn>(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        return NotReturn(typeof(TReturn), because, becauseArgs);
    }

    internal static string GetDescriptionFor(MethodInfo method)
    {
        if (method is null)
        {
            return string.Empty;
        }

        var returnTypeName = method.ReturnType.Name;

        return $"{returnTypeName} {method.DeclaringType}.{method.Name}";
    }

    private protected override string SubjectDescription => GetDescriptionFor(Subject);
    new protected string Identifier => "method";
}
