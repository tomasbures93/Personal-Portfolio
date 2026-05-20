using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Tests.UnitTests;

public class WebsiteConfigUnitTests
{
    private const string Email = "email@email.de";
    private const string UserName = "username";
    private List<Technology> Technologies = new List<Technology>()
    {
        new Technology("Technology 1", TechnologyCategory.Cloud)
    };
    
    [Fact]
    public void WebsiteConfigConstructor_WhenValidValuesAreGiven_ShouldInstantiateWebsiteConfig()
    {
        var config = new WebsiteConfig(UserName, Email);
        
        Assert.Equal(UserName, config.UserName);
        Assert.Equal(Email, config.Email);
    }

    [Theory]
    [InlineData(null)]    
    [InlineData("")]
    [InlineData(" ")]
    public void WebsiteConfigConstructor_WhenInvalidUserNameIsGiven_ShouldThrowException(string updateUserName)
    {
        Assert.Throws<DomainException>(() => new WebsiteConfig(updateUserName, Email));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void WebsiteConfigConstructor_WhenInvalidEmailIsGiven_ShouldThrowException(string updateEmail)
    {
        Assert.Throws<DomainException>(() => new WebsiteConfig(UserName, updateEmail));
    }

    [Fact]
    public void UpdateProfile_WhenValidValuesAreGiven_ShouldUpdateProfile()
    {
        string newEmail = "abc@abc.de";

        var config = new  WebsiteConfig(UserName, Email);
        
        config.UpdateProfil(newEmail, Technologies);

        Assert.Equal(newEmail, config.Email);
        Assert.NotNull(config.Technologies);
        Assert.Equal(Technologies[0].Name, config.Technologies.ElementAt(0).Name);
    }

    [Fact]
    public void UpdateProfile_WhenInvalidValuesAreGiven_ShouldThrowException()
    {
        var technologies = new List<Technology>();
        
        var config = new  WebsiteConfig(UserName, Email);

        Assert.Throws<DomainException>(() => config.UpdateProfil("", technologies));
    }
    
    [Fact]
    public void ChangeUserName_WhenValidValueIsGiven_ShouldChangeUserName()
    {
        var newUserName = "TestUser1";
        
        var config = new WebsiteConfig(UserName, Email);
        
        config.ChangeUserName(newUserName);

        Assert.Equal(newUserName, config.UserName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ChangeUserName_WhenInvalidValueIsGiven_ShouldThrowException(string invalidUserName)
    {
        var config = new WebsiteConfig(UserName, Email);

        Assert.Throws<DomainException>(() => config.ChangeUserName(invalidUserName));
    }

    [Fact]
    public void ChangeEmail_WhenValidValueIsGiven_ShouldChangeEmail()
    {
        var newEmail = "abc@abc";
        var config = new WebsiteConfig(UserName, Email);
        
        config.ChangeEmail(newEmail);

        Assert.Equal(newEmail, config.Email);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ChangeEmail_WhenInvalidValueIsGiven_ShouldThrowException(string invalidEmail)
    {
        var config  = new WebsiteConfig(UserName, Email);
        
        Assert.Throws<DomainException>(() => config.ChangeEmail(invalidEmail));
    }

    [Fact]
    public void ChangePassword_WhenValidValueIsGiven_ShouldChangePassword()
    {
        var config = new WebsiteConfig(UserName, Email);
        string hash = "ajgsfjasfka";

        config.UpdatePasswordHash(hash);
        
        Assert.Equal(hash, config.PasswordHash);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ChangePassword_WhenInvalidValueIsGiven_ShouldThrowException(string invalidPassword)
    {
        var config = new WebsiteConfig(UserName, Email);
        
        Assert.Throws<DomainException>(() => config.UpdatePasswordHash(invalidPassword));
    }
    
    [Fact]
    public void UpdateTechnologies_WhenValidValueIsGiven_ShouldUpdateTechnologies()
    {
        var config = new WebsiteConfig(UserName, Email);
        var updatedTechnologies = new List<Technology>()
        {
            new Technology("Technology 1", TechnologyCategory.Cloud),
            new Technology("Technology 2", TechnologyCategory.Framework)
        };
        
        config.UpdateProfil(Email, updatedTechnologies);
        
        Assert.Equal(updatedTechnologies.Count, config.Technologies.Count);
        Assert.Equal(updatedTechnologies[0].Name, config.Technologies.ElementAt(0).Name);
        Assert.Equal(updatedTechnologies[1].Name, config.Technologies.ElementAt(1).Name);
    }
}