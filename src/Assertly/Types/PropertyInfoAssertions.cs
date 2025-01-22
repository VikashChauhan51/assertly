using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using VReflector;


namespace Assertly.Types;
public class PropertyInfoAssertions(PropertyInfo propertyInfo) : MemberInfoAssertions<PropertyInfo, PropertyInfoAssertions>(propertyInfo)
{
    public AndConstraint<PropertyInfoAssertions> BeVirtual(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(Subject.IsVirtual())
        .BecauseOf(because, becauseArgs)
        .FailWith($"Expected {SubjectDescription} to be virtual{{reason}}, but it is not.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }
    public AndConstraint<PropertyInfoAssertions> NotBeVirtual([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
           .BecauseOf(because, becauseArgs)
           .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(!Subject.IsVirtual())
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected {SubjectDescription} not to be virtual{{reason}}, but it is.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> BeWritable(
        bool checkNonPublicSetter = false, bool checkInitSetter = false, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
          .BecauseOf(because, becauseArgs)
          .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(Subject!.SetIsAllowed(checkNonPublicSetter, checkInitSetter))
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected {SubjectDescription} to have a setter{{reason}}, but it is not.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> NotBeWritable(
        bool checkNonPublicSetter = false, bool checkInitSetter = false, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
         .BecauseOf(because, becauseArgs)
         .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(!Subject!.SetIsAllowed(checkNonPublicSetter, checkInitSetter))
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected {SubjectDescription} to not have a setter{{reason}}, but it has.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> BeReadable([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(Subject!.CanRead)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Expected property {SubjectDescription} to have a getter{{reason}}, but it does not.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> NotBeReadable(
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null)
      .BecauseOf(because, becauseArgs)
      .FailWith("Expected property to be virtual{reason}, but {context:property} is <null>.");

        ForCondition(!Subject!.CanRead)
        .BecauseOf(because, becauseArgs)
        .FailWith($"Did not expect {SubjectDescription} to have a getter{{reason}}.");

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> Return(Type propertyType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(propertyType);


        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type of property to be {0}{reason}, but {context:property} is <null>.", propertyType);

        ForCondition(Subject!.PropertyType == propertyType)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected type of property {2} to be {0}{reason}, but it is {1}.",
            propertyType, Subject.PropertyType, Subject);

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> Return<TReturn>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return Return(typeof(TReturn), because, becauseArgs);
    }

    public AndConstraint<PropertyInfoAssertions> NotReturn(Type propertyType,
        [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ArgumentNullException.ThrowIfNull(propertyType);

        ForCondition(Subject is not null)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected type of property not to be {0}{reason}, but {context:property} is <null>.", propertyType);

        ForCondition(Subject!.PropertyType != propertyType)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected type of property {1} not to be {0}{reason}, but it is.", propertyType, Subject);

        return new AndConstraint<PropertyInfoAssertions>(this);
    }

    public AndConstraint<PropertyInfoAssertions> NotReturn<TReturn>([StringSyntax("CompositeFormat")] string because = "",
        params object[] becauseArgs)
    {
        return NotReturn(typeof(TReturn), because, becauseArgs);
    }
    new protected string Identifier => "property";
}
