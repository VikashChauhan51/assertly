using Assertly.Core;
using System.Diagnostics.CodeAnalysis;


namespace Assertly.Types;
public class FunctionAssertions<T>(Func<T> subject) : DelegateAssertionsBase<Func<T>, FunctionAssertions<T>>(subject)
{


    protected void InvokeSubject()
    {
        Subject();
    }


    public AndWhichConstraint<FunctionAssertions<T>, T> NotThrow([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {

        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} not to throw{reason}, but found <null>.");

        T result = default;


        try
        {
            result = Subject!();
        }
        catch (Exception exception)
        {

            BecauseOf(because, becauseArgs)
            .FailWith("Did not expect any exception{reason}, but found {0}.", exception);

            result = default;
        }

        return new AndWhichConstraint<FunctionAssertions<T>, T>(this, result);
    }



}
