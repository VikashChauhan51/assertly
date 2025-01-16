

namespace Assertly.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        bool name = false;
        name.Should().BeTrue();

    }
}
