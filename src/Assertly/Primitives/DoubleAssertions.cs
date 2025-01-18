using Assertly.Core;

namespace Assertly.Primitives;

public class DoubleAssertions(double? value) : NumericAssertionsBase<double, DoubleAssertions>(value)
{

}
