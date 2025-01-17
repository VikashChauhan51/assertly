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
        name.Assert().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void BeNull_ShouldFail_WhenValueIsNotNull()
    {
        // Arrange
        bool name = true;

        // Act & Assert
        name.Assert().BeNull("Value should be null because of {0}", "a specific reason");
    }

    [Fact]
    public void Be_ShouldFail_WhenValueDoesNotMatchExpected()
    {
        // Arrange
        bool name = false;

        // Act & Assert
        name.Assert().Be(true, "Value should be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueMatchesUnexpected()
    {
        // Arrange
        bool name = true;

        // Act & Assert
        name.Assert().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsFalse()
    {
        // Arrange
        bool name = false;

        // Act & Assert
        name.Assert().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeFalse_ShouldFail_WhenValueIsTrue_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Assert().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void BeNull_ShouldFail_WhenValueIsNotNull_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Assert().BeNull("Value should be null because of {0}", "a specific reason");
    }

    [Fact]
    public void Be_ShouldFail_WhenValueDoesNotMatchExpected_Nullable()
    {
        // Arrange
        bool? name = false;

        // Act & Assert
        name.Assert().Be(true, "Value should be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueMatchesUnexpected_Nullable()
    {
        // Arrange
        bool? name = true;

        // Act & Assert
        name.Assert().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsFalse_Nullable()
    {
        // Arrange
        bool? name = false;

        // Act & Assert
        name.Assert().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeTrue_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Assert().BeTrue("Value should be true because of {0}", "a specific reason");
    }

    [Fact]
    public void BeFalse_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Assert().BeFalse("Value should be false because of {0}", "a specific reason");
    }

    [Fact]
    public void NotBe_ShouldFail_WhenValueIsNull_Nullable()
    {
        // Arrange
        bool? name = null;

        // Act & Assert
        name.Assert().NotBe(true, "Value should not be {0} because of {1}", true, "a specific reason");
    }

    [Fact]
    public void MethodReturningTrue_ShouldReturnTrue_WhenChecked()
    {
        // Act
        bool result = MethodReturningTrue();

        // Assert
        result.Assert().BeTrue("Method should return true because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningFalse_ShouldReturnFalse_WhenChecked()
    {
        // Act
        bool result = MethodReturningFalse();

        // Assert
        result.Assert().BeFalse("Method should return false because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningNull_ShouldReturnNull_WhenChecked()
    {
        // Act
        bool? result = MethodReturningNull();

        // Assert
        result.Assert().BeNull("Method should return null because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningTrue_ShouldFail_WhenCheckedForFalse()
    {
        // Act
        bool result = MethodReturningTrue();

        // Assert
        result.Assert().BeFalse("Method should return false because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningFalse_ShouldFail_WhenCheckedForTrue()
    {
        // Act
        bool result = MethodReturningFalse();

        // Assert
        result.Assert().BeTrue("Method should return true because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningNull_ShouldFail_WhenCheckedForTrue()
    {
        // Act
        bool? result = MethodReturningNull();

        // Assert
        result.Assert().BeTrue("Method should return true because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningNull_ShouldFail_WhenCheckedForFalse()
    {

        // Assert
        MethodReturningNull().Assert().BeFalse("Method should return false because of {0}", "a specific reason");
    }

    [Fact]
    public void MethodReturningFalse_ShouldFail_WhenCheckedForNull()
    {
        var result = new BoolenData();
        // Assert
        result.NestedTypeName.MoreNestedName.MethodReturning(false).Assert().BeNull("Method should return null because of {0}", "a specific reason");
    }
    private bool MethodReturningTrue()
    {
        return true;
    }

    private bool MethodReturningFalse()
    {
        return false;
    }

    private bool? MethodReturningNull()
    {
        return null;
    }


    public class BoolenData
    {
        public NestedType NestedTypeName { get; set; }= new NestedType();
        public bool MethodReturningFalse()
        {
            return false;
        }


       public class NestedType
        {
            public MoreNested MoreNestedName { get; set; } = new MoreNested();
            public bool MethodReturningFalse()
            {
                return false;
            }

            public class MoreNested
            {
                public bool MethodReturning(bool value)
                {
                    return value;
                }
            }
        }
    }
}
