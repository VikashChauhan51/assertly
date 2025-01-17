using Assertly.Core;
using System.Diagnostics.CodeAnalysis;

namespace Assertly.Primitives;
public class EnumAssertions<TEnum>(TEnum value) : EnumAssertionsBase<TEnum>(value) where TEnum : Enum
{
}

public class EnumAssertionsBase<TEnum>(TEnum value) : AssertionsBase<TEnum>(value) where TEnum : Enum
{
 
    public AndConstraint<EnumAssertions<TEnum>> Be(TEnum expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Nullable.Equals(Subject, expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} to be {0} {reason}, but found {1}.", expected, EnsureSubject());

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> NotBe(TEnum unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(!Nullable.Equals(Subject, unexpected))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} not to be {0} {reason}, but it was.", unexpected);

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

  
    public AndConstraint<EnumAssertions<TEnum>> BeOneOf(IEnumerable<TEnum> validValues, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(validValues.Contains(Subject))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} to be one of {0} {reason}, but found {1}.", validValues, EnsureSubject());

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> BeOneOf(params TEnum[] validValues)
    {
        return BeOneOf(validValues.AsEnumerable());
    }

    public AndConstraint<EnumAssertions<TEnum>> HaveValue(int expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        int value = Convert.ToInt32(Subject);
        ForCondition(value == expected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} to have value {0} {reason}, but found {1}.", expected, value);

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> NotHaveValue(int unexpected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        int value = Convert.ToInt32(Subject);
        ForCondition(value != unexpected)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} not to have value {0} {reason}, but found {1}.", unexpected, value);

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> BeDefined([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        bool isDefined = Subject != null && Enum.IsDefined(typeof(TEnum), Subject);
        ForCondition(isDefined)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} to be defined, but it was not defined.");

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> NotBeDefined([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        bool isDefined = Subject != null && Enum.IsDefined(typeof(TEnum), Subject);
        ForCondition(!isDefined)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} not to be defined, but it was defined.");

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }
    public AndConstraint<EnumAssertions<TEnum>> HaveFlag(TEnum expectedFlag, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        int value = Convert.ToInt32(Subject);
        int flag = Convert.ToInt32(expectedFlag);
        bool hasFlag = (value & flag) == flag;
        ForCondition(hasFlag)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} to have flag {0} {reason}, but it did not.", expectedFlag);

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }

    public AndConstraint<EnumAssertions<TEnum>> NotHaveFlag(TEnum unexpectedFlag, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        int value = Convert.ToInt32(Subject);
        int flag = Convert.ToInt32(unexpectedFlag);
        bool hasFlag = (value & flag) != flag;
        ForCondition(hasFlag)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected {context:enum} not to have flag {0} {reason}, but it did.", unexpectedFlag);

        return new AndConstraint<EnumAssertions<TEnum>>((EnumAssertions<TEnum>)this);
    }
}

