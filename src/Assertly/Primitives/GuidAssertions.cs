using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class GuidAssertions : GuidAssertions<GuidAssertions>
{
    public GuidAssertions(Guid? value) : base(value) { }
}

public class GuidAssertions<TAssertions> : AssertionsBase<Guid?>
    where TAssertions : GuidAssertions<TAssertions>
{
    public GuidAssertions(Guid? value) : base(value) { }

    public AndConstraint<TAssertions> Be(Guid expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:guid} to be {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBe(Guid unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:guid} not to be {0} {reason}, but it is.", unexpected);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeEmpty([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject == Guid.Empty)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:guid} to be {0} {reason}, but found {1}.", Guid.Empty, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> NotBeEmpty([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject != Guid.Empty)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:guid} not to be {0} {reason}, but it is.", Guid.Empty);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(IEnumerable<Guid?> validValues, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:guid} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeOneOf(params Guid[] validValues)
    {
        return BeOneOf(validValues.Cast<Guid?>());
    }
}

