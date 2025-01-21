using Assertly.Primitives;

namespace Assertly.Test.Primitives;


[Trait("Category", "Unit")]
public class Int16AssertionsTests
{
    [Fact]
    public void Be_ShouldNotThrow_WhenValueMatchesExpected()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.Be(10, "because it should match");
    }

    [Fact]
    public void Be_ShouldThrow_WhenValueDoesNotMatchExpected()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.Be(5, "because it should not match"));
    }

    [Fact]
    public void NotBe_ShouldNotThrow_WhenValueDoesNotMatchUnexpected()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.NotBe(5, "because it should not match");
    }

    [Fact]
    public void NotBe_ShouldThrow_WhenValueMatchesUnexpected()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.NotBe(10, "because it should not match"));
    }

    [Fact]
    public void BePositive_ShouldNotThrow_WhenValueIsPositive()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.BePositive("because it should be positive");
    }

    [Fact]
    public void BePositive_ShouldThrow_WhenValueIsNegative()
    {
        // Arrange
        short? value = -10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.BePositive("because it should be positive"));
    }

    [Fact]
    public void BeNegative_ShouldNotThrow_WhenValueIsNegative()
    {
        // Arrange
        short? value = -10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.BeNegative("because it should be negative");
    }

    [Fact]
    public void BeNegative_ShouldThrow_WhenValueIsPositive()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.BeNegative("because it should be negative"));
    }

    [Fact]
    public void BeLessThan_ShouldNotThrow_WhenValueIsLessThanExpected()
    {
        // Arrange
        short? value = 5;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.BeLessThan(10, "because it should be less");
    }

    [Fact]
    public void BeLessThan_ShouldThrow_WhenValueIsNotLessThanExpected()
    {
        // Arrange
        short? value = 15;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.BeLessThan(10, "because it should be less"));
    }

    [Fact]
    public void BeInRange_ShouldNotThrow_WhenValueIsWithinRange()
    {
        // Arrange
        short? value = 7;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.BeInRange(5, 10, "because it should be in range");
    }

    [Fact]
    public void BeInRange_ShouldThrow_WhenValueIsOutsideRange()
    {
        // Arrange
        short? value = 15;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.BeInRange(5, 10, "because it should be in range"));
    }

    [Fact]
    public void BeOneOf_ShouldNotThrow_WhenValueIsInList()
    {
        // Arrange
        short? value = 10;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        assertions.BeOneOf(5, 10, 15);
    }

    [Fact]
    public void BeOneOf_ShouldThrow_WhenValueIsNotInList()
    {
        // Arrange
        short? value = 20;
        var assertions = new Int16Assertions(value);

        // Act & Assert
        Assert.Throws<AssertlyException>(() =>
            assertions.BeOneOf(5, 10, 15));
    }
}


