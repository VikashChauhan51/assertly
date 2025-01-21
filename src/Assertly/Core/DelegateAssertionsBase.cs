using System.Diagnostics.CodeAnalysis;


namespace Assertly.Core;
public abstract class DelegateAssertionsBase<TDelegate, TAssertions>(TDelegate @delegate)
    : ReferenceTypeAssertions<TDelegate, DelegateAssertionsBase<TDelegate, TAssertions>>(@delegate)
    where TDelegate : Delegate
    where TAssertions : DelegateAssertionsBase<TDelegate, TAssertions>
{


    protected AndConstraint<TAssertions> Throw(Exception exception, [StringSyntax("CompositeFormat")] string because,
       object[] becauseArgs)
    {

        ForCondition(exception is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect any exception {reason}, but found {0}.", exception);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    protected AndConstraint<TAssertions> Throw<TException>(Exception exception, [StringSyntax("CompositeFormat")] string because, object[] becauseArgs)
        where TException : Exception
    {
        Throw(exception, because, becauseArgs);
        ForCondition(exception.GetType().IsAssignableTo(typeof(TException)))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {0}{reason}, but found {1}.", typeof(TException), exception);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
    protected AndConstraint<TAssertions> NotThrow(Exception exception, [StringSyntax("CompositeFormat")] string because,
        object[] becauseArgs)
    {

        ForCondition(exception is null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect any exception {reason}, but found {0}.", exception);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    protected AndConstraint<TAssertions> NotThrow<TException>(Exception exception, [StringSyntax("CompositeFormat")] string because, object[] becauseArgs)
        where TException : Exception
    {
        NotThrow(exception, because, becauseArgs);
        ForCondition(!exception.GetType().IsAssignableTo(typeof(TException)))
        .BecauseOf(because, becauseArgs)
        .FailWith("Did not expect {0}{reason}, but found {1}.", typeof(TException), exception);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
