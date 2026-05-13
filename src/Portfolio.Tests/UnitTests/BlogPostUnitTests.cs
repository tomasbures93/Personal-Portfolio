using Portfolio.Domain.Entities;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Tests.UnitTests;

public class BlogPostUnitTests
{
    private const int Id = 1;
    private const string Title = "Test Title";
    private const string Content = "I am test content";
    private const bool Draft = false;
    private const string Creator = "Tomas";
    
    [Fact]
    public void BlogPostConstructor_WhenValidCreateValuesAreGiven_ShouldInstantiateBlogPost()
    {
        // Act
        var blogPost = new BlogPost(Title, Content, Draft, Creator);

        // Assert
        Assert.Equal(Title, blogPost.Title);
        Assert.Equal(Content, blogPost.Content);
        Assert.Equal(Draft, blogPost.Draft);
        Assert.Equal(Creator, blogPost.Creator);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void BlogPostConstructor_WhenInvalidTitleIsGiven_ShouldThrowException(string titleTestData)
    {
        // Assert
        Assert.Throws<DomainException>(() => new BlogPost(titleTestData, Content, Draft, Creator));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void BlogPostConstructor_WhenInvalidContentIsGiven_ShouldThrowException(string contentTestData)
    {
        // Assert
        Assert.Throws<DomainException>(() => new BlogPost(Title, contentTestData, Draft, Creator));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void BlogPostConstructor_WhenInvalidCreatorIsGiven_ShouldThrowException(string creatorTestData)
    {
        // Assert
        Assert.Throws<DomainException>(() => new BlogPost(Title, Content, Draft, creatorTestData));
    }

    [Fact]
    public void Update_WhenValidDataIsGiven_ShouldUpdateBlogPost()
    {
        // Arrange
        var blogPost = new BlogPost(Title, Content, Draft, Creator);
        var newTitle = "I am new title";
        var newContent = "I am new content";
        var differentDraft = true;
        
        // Act
        blogPost.Update(newTitle, newContent, differentDraft);
        
        // Assert
        Assert.Equal(newTitle, blogPost.Title);
        Assert.Equal(newContent, blogPost.Content);
        Assert.Equal(differentDraft, blogPost.Draft);
    } 
    
    [Fact]
    public void BlogPostConstructor_WhenValidExistingValuesAreGiven_ShouldInstantiateBlogPost()
    {
        // Act
        var blogPost = new BlogPost(Id, Title, Content, Draft);
        
        // Assert
        Assert.Equal(Id, blogPost.Id);
        Assert.Equal(Title, blogPost.Title);
        Assert.Equal(Content, blogPost.Content);
        Assert.Equal(Draft, blogPost.Draft);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void BlogPostConstructor_WhenInvalidIdIsGiven_ShouldThrowException(int idTestData)
    {
        // Assert
        Assert.Throws<DomainException>(() => new BlogPost(idTestData, Title, Content, Draft));
    }
}