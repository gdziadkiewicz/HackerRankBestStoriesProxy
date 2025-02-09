namespace HackerRankBestStoriesProxy.Tests;

public class HackerNewsStoryDeserializerTests
{
    [Test]
    public void Deserialize_ValidJson_ReturnsHackerNewsStory()
    {
        // Arrange
        string json = @"{
            ""title"": ""Test Story"",
            ""url"": ""http://example.com"",
            ""by"": ""testuser"",
            ""time"": 1570881781,
            ""score"": 100,
            ""descendants"": 50
        }";
        var expectedStory = new HackerNewsStory
        {
            Title = "Test Story",
            Url = "http://example.com",
            By = "testuser",
            Time = 1570881781,
            Score = 100,
            Descendants = 50
        };

        // Act & Assert
        TestDeserialize(json, expectedStory);
    }

    [Test]
    public void Deserialize_ValidJson_NoURL_ReturnsHackerNewsStory()
    {
        // Arrange
        string json = @"{
            ""title"": ""Test Story"",
            ""by"": ""testuser"",
            ""time"": 1570881781,
            ""score"": 100,
            ""descendants"": 50
        }";
        var expectedStory = new HackerNewsStory
        {
            Title = "Test Story",
            Url = null,
            By = "testuser",
            Time = 1570881781,
            Score = 100,
            Descendants = 50
        };

        // Act & Assert
        TestDeserialize(json, expectedStory);
    }

    private void TestDeserialize(string json, HackerNewsStory expectedStory)
    {
        // Act
        var story = System.Text.Json.JsonSerializer.Deserialize<HackerNewsStory>(json, System.Text.Json.JsonSerializerOptions.Web);

        // Assert
        Assert.That(story, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(story.Title, Is.EqualTo(expectedStory.Title));
            Assert.That(story.Url, Is.EqualTo(expectedStory.Url));
            Assert.That(story.By, Is.EqualTo(expectedStory.By));
            Assert.That(story.Time, Is.EqualTo(expectedStory.Time));
            Assert.That(story.Score, Is.EqualTo(expectedStory.Score));
            Assert.That(story.Descendants, Is.EqualTo(expectedStory.Descendants));
        });
    }
}