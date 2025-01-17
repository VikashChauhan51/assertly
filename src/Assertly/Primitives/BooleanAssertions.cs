using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class BooleanAssertions(bool? value, AssertionChain assertionChain) :
    BooleanAssertions<BooleanAssertions>(value, assertionChain)
{
}

public class BooleanAssertions<TAssertions>
    where TAssertions : BooleanAssertions<TAssertions>
{
    private readonly bool? subject;
    private readonly AssertionChain assertionChain;
    public BooleanAssertions(bool? value, AssertionChain assertionChain)
    {
        subject = value;
        this.assertionChain = assertionChain;
    }

    public AndConstraint<TAssertions> BeTrue([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(subject == true)
            .BecauseOf(because, becauseArgs)
            .FailWith(true, subject != null ? subject : "Null")
            .Validation();



        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeFalse([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(subject == false)
            .BecauseOf(because, becauseArgs)
            .FailWith(false, subject != null ? subject : "Null")
            .Validation();

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeNull([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(subject == null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Null", subject)
            .Validation();

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


    public AndConstraint<TAssertions> Be(bool? expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith(expected != null ? expected : "Null", subject != null ? subject : "Null")
            .Validation();

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(bool? unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        assertionChain
            .ForCondition(subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith(unexpected != null ? unexpected : "Null", subject != null ? subject : "Null")
            .Validation();

        return new AndConstraint<TAssertions>((TAssertions)this);
    }


}
