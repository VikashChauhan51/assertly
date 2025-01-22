using Assertly.Core;
using System.Reflection;


namespace Assertly.Types;
public class ConstructorInfoAssertions(ConstructorInfo constructorInfo) : MethodBaseAssertions<ConstructorInfo, ConstructorInfoAssertions>(constructorInfo)
{

    private protected override string SubjectDescription => GetDescriptionFor(Subject);
    new protected string Identifier => "constructor";

    private static string GetDescriptionFor(ConstructorInfo constructorInfo)
    {
        return $"{constructorInfo.DeclaringType}({GetParameterString(constructorInfo)})";
    }
}

