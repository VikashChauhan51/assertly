namespace Assertly.Primitives;
public class BooleanAssertions : BooleanAssertions<BooleanAssertions>
{

    public BooleanAssertions(bool value, AssertionChain assertionChain) : base(value, assertionChain)
    {

    }


}


public class BooleanAssertions<TAssertions> where TAssertions : BooleanAssertions<TAssertions>
{
    private readonly bool subject;
    private readonly AssertionChain assertionChain;
    public BooleanAssertions(bool value, AssertionChain assertionChain)
    {
        subject = value;
        this.assertionChain = assertionChain;
    }

    public AndConstraint<TAssertions> BeTrue()
    {
        assertionChain
             .ForCondition(subject)
             .FailWith( true, subject);

        Execute();
        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    private void Execute()
    {
        if (assertionChain.HasFailures)
        {
            var message = assertionChain.GetFailures();
            throw TrueException.ForNonTrueValue(subject, message);
        }

    }
    public AndConstraint<TAssertions> BeFalse()
    {

        if (subject)
        {
            throw TrueException.ForNonTrueValue(subject);
        }
        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
