using Portfolio.Domain.Common;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Tests;

public class GuardUnitTests
{
    private readonly string _paramName = "testing";
    [Fact]
    public void Test_AgainstNullOrWhiteSpace_NullInput_ThrowsDomainException()
    {
        // Arrange 
        
        // Act
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(null, _paramName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_EmptyInput_ThrowsDomainException()
    {
        // Arrange
        string input = string.Empty;
        
        // Act
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(input, _paramName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_WhiteSpaceInput_ThrowsDomainException()
    {
        // Arrange
        string input = "  ";

        // Act

        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(input, _paramName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_ValidInput_DoesNotThrowsDomainException()
    {
        // Arrange
        string input = "Is it working?";

        // Act
        var exception = Record.Exception(() => Guard.AgainstNullOrWhiteSpace(input, _paramName));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Test_ValidId_InvalidIdInput_ThrowsDomainException(int invalidId)
    {
        // Arrange
        
        // Act
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.ValidId(invalidId, _paramName));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(int.MaxValue)]
    public void Test_ValidId_ValidIdInput_DoesNotThrowsDomainException(int validId)
    {
        // Arrange
        
        // Act
        var exception = Record.Exception(() => Guard.ValidId(validId, _paramName));
        
        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Test_EnumValueExists_ThrowsDomainException()
    {
        // Arrange
        
        
        // Act
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.EnumValueExists((TechnologyCategory)999, _paramName));
    }

    [Fact]
    public void Test_EnumValueExists_DoesNotThrowsDomainException()
    {
        // Arrange
        var input = (TechnologyCategory)2;

        // Act
        var exception = Record.Exception(() => Guard.EnumValueExists(input, _paramName));
        
        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Test_TechnologiesAreNotEmpty_ThrowsDomainException()
    {
        // Arrange
        var list = new List<Technology>();
        
        // Act

        // Assert
        Assert.Throws<DomainException>(() => Guard.TechnologiesAreNotEmpty(list, _paramName));
    }

    [Fact]
    public void Test_TechnologiesAreNotEmpty_DoesNotThrowsDomainException()
    {
        // Arrange
        var list = new List<Technology>{ new Technology("Technology 1", TechnologyCategory.Framework) };
        
        // Act
        var exception = Record.Exception(() => Guard.TechnologiesAreNotEmpty(list, _paramName));
        
        // Assert
        Assert.Null(exception);
    }
}