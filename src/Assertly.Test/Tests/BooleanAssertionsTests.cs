namespace Assertly.Test.Tests;

[Trait("Category","Unint")]
public class BooleanAssertionsTests
{
    [Fact]
    public void BeFalse_ShouldFail_WhenValueIsTrue()
    {
        // Arrange
        bool name = true;

        // Act & Assert
        name.Should().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void BeNull_ShouldFail_WhenValueIsNotNull()
    {
        // Arrange
        bool name = true;

        // Act & Assert
        name.Should().BeNull("Value should be null because of {0}", "a specific reason");
    }

    [Fact]
    public void Be_ShouldFail_WhenValueDoesNotMatchExpected()
    {
        // Arrange
        bool name = false;

        // Act & Assert
        name.Should().Be(true, "Value should be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueMatchesUnexpected()
    {
        // Arrange
        bool name = true;

        // Act & Assert
        name.Should().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsFalse()
    {
        // Arrange
        bool name = false;

        // Act & Assert
        name.Should().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeFalse_ShouldFail_WhenValueIsTrue_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Should().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void BeNull_ShouldFail_WhenValueIsNotNull_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Should().BeNull("Value should be null because of {0}", "a specific reason");
    }

    [Fact]
    public void Be_ShouldFail_WhenValueDoesNotMatchExpected_Nullable()
    {
        // Arrange
        bool? name = false;

        // Act & Assert
        name.Should().Be(true, "Value should be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueMatchesUnexpected_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Should().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsFalse_Nullable()
    {
        // Arrange
        bool? name = false;

        // Act & Assert
        name.Should().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Should().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeFalse_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Should().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Should().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }
}
