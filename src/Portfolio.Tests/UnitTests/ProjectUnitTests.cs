using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;

namespace Portfolio.Tests.UnitTests;

public class ProjectUnitTests
{
    private const int Id = 1;
    private const string Title = "Project Title";
    private const string Description = "Project Description";
    private const string Url = "Project Url";
    private List<Technology> Technologies = new List<Technology>()
    {
        new Technology("Test", TechnologyCategory.Framework)
    };
    
    [Fact]
    public void ProjectConstructor_WhenValidCreateValuesAreGiven_ShouldInstantiateProject()
    {
        // Act
        var project = new Project(Title, Description, Technologies, Url);
        
        // Assert
        Assert.Equal(Title, project.Title);
        Assert.Equal(Description, project.Description);
        Assert.Equal(Url, project.Url);
        Assert.NotNull(project.Technologies);
    }

    [Fact]
    public void ProjectConstructorWithId_WhenValidCreateValuesAreGiven_ShouldInstantiateProject()
    {
        // Act
        var project = new Project(Id, Title, Description, Technologies, Url);
        
        // Assert
        Assert.Equal(Id, project.Id);
        Assert.Equal(Title, project.Title);
        Assert.Equal(Description, project.Description);
        Assert.Equal(Url, project.Url);
        Assert.NotNull(project.Technologies);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]    
    [InlineData(" ")]
    public void ProjectConstructor_WhenInvalidTitleIsGiven_ShouldThrowException(string testTitle)
    {
        Assert.Throws<DomainException>(() => new Project(testTitle, Description, Technologies, Url));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ProjectConstructor_WhenInvalidDescriptionIsGiven_ShouldThrowException(string testDescription)
    {
        Assert.Throws<DomainException>(() => new Project(Title, testDescription, Technologies, Url));
    }

    [Fact]
    public void ProjectConstructor_WhenNoTechnologiesAreGiven_ShouldThrowException()
    {
        var technologies = new List<Technology>();
        
        Assert.Throws<DomainException>(() => new Project(Title, Description, technologies, Url));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public void ProjectConstructor_WhenInvalidIdIsGiven_ShouldThrowException(int id)
    {
        Assert.Throws<DomainException>(() => new Project(id,  Title, Description, Technologies, Url));
    }

    [Fact]
    public void Update_WhenValidValuesAreGiven_ShouldUpdateTechnologies()
    {
        // Arrange
        var project =  new Project(Id, Title, Description, Technologies, Url);

        var updateTitle = "Title";
        var updateDescription = "Description";
        var updateUrl = "Url";
        var updateTechnologies = new List<Technology>()
        {
            new Technology("Technology 1", TechnologyCategory.Cloud),
            new Technology("Technology 2", TechnologyCategory.Framework),
        };
        
        // Act
        project.Update(updateTitle, updateDescription, updateTechnologies, updateUrl);
        
        // Assert
        Assert.Equal(updateTitle, project.Title);
        Assert.Equal(updateDescription, project.Description);
        Assert.Equal(updateUrl, project.Url);
        Assert.NotNull(project.Technologies);
        Assert.Equal(updateTechnologies.Count, project.Technologies.Count);
        Assert.Equal(updateTechnologies[0].Name, project.Technologies.ElementAt(0).Name);
        Assert.Equal(updateTechnologies[1].Name, project.Technologies.ElementAt(1).Name);
    }
}