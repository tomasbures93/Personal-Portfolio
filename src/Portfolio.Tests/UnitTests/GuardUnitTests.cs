using Portfolio.Domain.Common;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Tests.UnitTests;

public class GuardUnitTests
{
    private const string ParamName = "testing";
    
    [Fact]
    public void Test_AgainstNullOrWhiteSpace_NullInput_ThrowsDomainException()
    {
        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(null, ParamName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_EmptyInput_ThrowsDomainException()
    {
        // Arrange
        var input = string.Empty;
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(input, ParamName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_WhiteSpaceInput_ThrowsDomainException()
    {
        // Arrange
        string input = "  ";
        
        // Assert
        Assert.Throws<DomainException>(() => Guard.AgainstNullOrWhiteSpace(input, ParamName));
    }

    [Fact]
    public void Test_AgainstNullOrWhiteSpace_ValidInput_DoesNotThrowsDomainException()
    {
        // Arrange
        var input = "Is it working?";

        // Act
        var exception = Record.Exception(() => Guard.AgainstNullOrWhiteSpace(input, ParamName));

        // Assert
        Assert.Null(exception);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void Test_ValidId_InvalidIdInput_ThrowsDomainException(int invalidId)
    {
        // Assert
        Assert.Throws<DomainException>(() => Guard.ValidId(invalidId, ParamName));
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(int.MaxValue)]
    public void Test_ValidId_ValidIdInput_DoesNotThrowsDomainException(int validId)
    {
        // Act
        var exception = Record.Exception(() => Guard.ValidId(validId, ParamName));
        
        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Test_EnumValueExists_ThrowsDomainException()
    {
        // Assert
        Assert.Throws<DomainException>(() => Guard.EnumValueExists((TechnologyCategory)999, ParamName));
    }

    [Fact]
    public void Test_EnumValueExists_DoesNotThrowsDomainException()
    {
        // Arrange
        var enumeration = (TechnologyCategory)2;

        // Act
        var exception = Record.Exception(() => Guard.EnumValueExists(enumeration, ParamName));
        
        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Test_TechnologiesAreNotEmpty_ThrowsDomainException()
    {
        // Arrange
        var list = new List<Technology>();

        // Assert
        Assert.Throws<DomainException>(() => Guard.TechnologiesAreNotEmpty(list, ParamName));
    }

    [Fact]
    public void Test_TechnologiesAreNotEmpty_DoesNotThrowsDomainException()
    {
        // Arrange
        var list = new List<Technology>{ new Technology("Technology 1", TechnologyCategory.Framework) };
        
        // Act
        var exception = Record.Exception(() => Guard.TechnologiesAreNotEmpty(list, ParamName));
        
        // Assert
        Assert.Null(exception);
    }
}