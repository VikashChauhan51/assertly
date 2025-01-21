using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using VReflector;

namespace Assertly.Types;
public class ExceptionAssertions<TException>(IEnumerable<TException> exceptions) : ReferenceTypeAssertions<IEnumerable<TException>, ExceptionAssertions<TException>>(exceptions)
    where TException : Exception
{
    public TException And => SingleSubject;

    public TException Which => And;

    public virtual ExceptionAssertions<TException> WithMessage(string expectedWildcardPattern,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is null || !exceptions.Any())
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected exception with message {0} {reason}, but no exception was thrown.", expectedWildcardPattern);

        AssertExceptionMessage(Subject.Select(exc => exc.Message), expectedWildcardPattern, because,
            becauseArgs);

        return this;
    }

    public virtual ExceptionAssertions<TInnerException> WithInnerException<TInnerException>(string because = "",
        params object[] becauseArgs)
        where TInnerException : Exception
    {
        var expectedInnerExceptions = AssertInnerExceptions(typeof(TInnerException), because, becauseArgs);
        return new ExceptionAssertions<TInnerException>(expectedInnerExceptions.Cast<TInnerException>());
    }

    public ExceptionAssertions<Exception> WithInnerException(Type innerException, string because = "",
        params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(innerException);

        return new ExceptionAssertions<Exception>(AssertInnerExceptions(innerException, because, becauseArgs));
    }

    public virtual ExceptionAssertions<TInnerException> WithInnerExceptionExactly<TInnerException>(string because = "",
        params object[] becauseArgs)
        where TInnerException : Exception
    {
        var exceptionExpression = AssertInnerExceptionExactly(typeof(TInnerException), because, becauseArgs);
        return new ExceptionAssertions<TInnerException>(exceptionExpression.Cast<TInnerException>());
    }

    public ExceptionAssertions<Exception> WithInnerExceptionExactly(Type innerException, string because = "",
        params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(innerException);

        return new ExceptionAssertions<Exception>(AssertInnerExceptionExactly(innerException, because, becauseArgs));
    }
    public ExceptionAssertions<TException> Where(Expression<Func<TException, bool>> exceptionExpression,
        string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(exceptionExpression);

        Func<TException, bool> condition = exceptionExpression.Compile();

        
            ForCondition(condition(SingleSubject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected exception where {0}{reason}, but the condition was not met by:"
                        + Environment.NewLine + Environment.NewLine + "{1}.",
                exceptionExpression, Subject);

        return this;
    }

    private IEnumerable<Exception> AssertInnerExceptionExactly(Type innerException, string because = "",
        params object[] becauseArgs)
    {
        
            BecauseOf(because, becauseArgs)
            .ForCondition(Subject is not null && Subject.Any(e => e.InnerException is not null))
            .FailWith("Expected inner {0}{reason}, but the thrown exception has no inner exception.", innerException);

        Exception[] expectedExceptions = Subject?
            .Select(e => e.InnerException)
            .Where(e => e?.GetType() == innerException).ToArray();

        
            ForCondition(expectedExceptions?.Length > 0)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected inner {0}{reason}, but found {1}.", innerException, SingleSubject.InnerException);

        return expectedExceptions;
    }

    private IEnumerable<Exception> AssertInnerExceptions(Type innerException, string because = "",
        params object[] becauseArgs)
    {
        
            BecauseOf(because, becauseArgs)
            .ForCondition(Subject.Any(e => e.InnerException is not null))
            .FailWith("Expected inner {0}{reason}, but the thrown exception has no inner exception.", innerException);

        Exception[] expectedInnerExceptions = Subject
            .Select(e => e.InnerException)
            .Where(e => e != null && e.GetType().IsSameOrInherits(innerException))
            .ToArray();

        
            ForCondition(expectedInnerExceptions.Length > 0)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected inner {0}{reason}, but found {1}.", innerException, SingleSubject.InnerException);

        return expectedInnerExceptions;
    }

    private TException SingleSubject
    {
        get
        {
            if (Subject.Count() > 1)
            {
                string thrownExceptions = BuildExceptionsString(Subject);

                throw new AssertlyException($"More than one exception was thrown.  Assertly cannot determine which Exception was meant.{Environment.NewLine}{thrownExceptions}");

            }

            return Subject.Single();
        }
    }

    private static string BuildExceptionsString(IEnumerable<TException> exceptions)
    {
        return string.Join(Environment.NewLine,
            exceptions.Select(
                exception =>
                    "\t" + exception.ToString()));
    }

    private void AssertExceptionMessage(IEnumerable<string> messages, string expectation, string because, params object[] becauseArgs)
    {
        //TODO:pending
    }
}
