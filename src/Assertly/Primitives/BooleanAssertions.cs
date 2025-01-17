using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class BooleanAssertions(bool? value) :
    BooleanAssertions<BooleanAssertions>(value)
{
}

public class BooleanAssertions<TAssertions>(bool? value) : AssertionsBase<bool?>(value)
    where TAssertions : BooleanAssertions<TAssertions>
{

    public AndConstraint<TAssertions> BeTrue([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == true)
             .BecauseOf(because, becauseArgs)
             .FailWith("Expected {context:boolean} to be {0} {reason}, but found {1}.", true, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeFalse([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == false)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:boolean} to be {0} {reason}, but found {1}.", false, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNull([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:boolean} to be {0} {reason}, but found {1}.", AssertionConstants.Null, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> Be(bool? expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:boolean} to be {0} {reason}, but found {1}.", AssertionHelper.EnsureType(expected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(bool? unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:boolean} not to be {0} {reason}, but found {1}.", AssertionHelper.EnsureType(unexpected), EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
