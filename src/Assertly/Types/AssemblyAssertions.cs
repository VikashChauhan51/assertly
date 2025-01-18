using Assertly.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Assertly.Types;
public class AssemblyAssertions(Assembly? subject) : AssemblyAssertions<Assembly, AssemblyAssertions>(subject)
{

}

public class AssemblyAssertions<TSubject, TAssertions>(TSubject? subject) : ReferenceTypeAssertions<TSubject, TAssertions>(subject)
    where TSubject : Assembly
    where TAssertions : AssemblyAssertions<TSubject, TAssertions>
{

    public AndConstraint<TAssertions> HaveName(string expectedName, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && Subject.GetName().Name == expectedName)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to have name {0}{reason}, but found {1}.", expectedName, Subject?.GetName().Name);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
 
    public AndConstraint<TAssertions> HaveVersion(string expectedVersion, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && Subject.GetName().Version.ToString() == expectedVersion)
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to have version {0}{reason}, but found {1}.", expectedVersion, Subject?.GetName().Version);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> ContainAttribute<TAttribute>([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && Subject.GetCustomAttributes(typeof(TAttribute), false).Any())
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to contain attribute {0}{reason}, but it was not found.", typeof(TAttribute).Name);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeEquivalentTo(Assembly expected, [StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(expected is not null && Subject is not null && expected.Equals(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be equivalent to {0}{reason}, but found {1}.", expected.FullName, Subject?.FullName);

        return new AndConstraint<TAssertions>((TAssertions)this);
    }

    public AndConstraint<TAssertions> BeLoaded([StringSyntax("CompositeFormat")] string because = "", params object[] becauseArgs)
    {
        ForCondition(Subject is not null && AppDomain.CurrentDomain.GetAssemblies().Contains(Subject))
        .BecauseOf(because, becauseArgs)
        .FailWith("Expected {context} to be loaded{reason}, but it was not found in the current AppDomain.");

        return new AndConstraint<TAssertions>((TAssertions)this);
    }
}
